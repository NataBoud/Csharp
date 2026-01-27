// details - produit.js


}


document.addEventListener('DOMContentLoaded', () => {
    const alertContainer = document.getElementById('alertes-stock');

    function showStockAlert(message) {
        if (!alertContainer) return;

        alertContainer.innerHTML = `
        <div class="alert alert-warning fade show">
            ${message}
        </div>
    `;

    }

    function hideStockAlert() {
        if (!alertContainer) return;
        alertContainer.innerHTML = '';
    } // <-- fermeture correcte de la fonction

    const btnAjouter = document.querySelector('.btn-ajouter-panier');
    const btnQuantiteDiv = document.querySelector('.btn-quantite-panier');
    if (!btnAjouter || !btnQuantiteDiv) return;

    const compteurSpan = btnQuantiteDiv.querySelector('.compteur-panier');
    const btnPlus = btnQuantiteDiv.querySelector('.btn-plus');
    const btnMoins = btnQuantiteDiv.querySelector('.btn-moins');

    const produitId = btnAjouter.dataset.produitId;
    const stockMax = parseInt(btnAjouter.dataset.stock) || 0;

    //  cacher le contrôle quantité par défaut
    btnQuantiteDiv.style.display = 'none';
    btnAjouter.style.display = 'inline-block'; // s'assurer que le bouton Ajouter est visible

    // si déjà dans le panier
    fetch(`/Panier/QuantiteProduit?produitId=${produitId}`)
        .then(r => r.json())
        .then(({ quantite }) => {
            if (quantite > 0) {
                btnAjouter.style.display = 'none';
                btnQuantiteDiv.style.display = 'flex';
                compteurSpan.innerText = quantite;
                if (quantite >= stockMax) btnPlus.disabled = true;
            }
        });

    // premier ajout
    btnAjouter.addEventListener('click', () => {
        fetch('/Panier/AjouterAjax', {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            body: `produitId=${produitId}`
        })
            .then(r => r.json())
            .then(data => {
                if (!data.success) {
                    showStockAlert('Produit en rupture de stock');
                    return;
                }

                btnAjouter.style.display = 'none';
                btnQuantiteDiv.style.display = 'flex';
                compteurSpan.innerText = 1;

                if (stockMax === 1) {
                    btnPlus.disabled = true;
                    showStockAlert('Dernier article disponible');
                }

                updatePanierBadge();
            });
    });


    // +
    btnPlus.addEventListener('click', () => {
        fetch('/Panier/AjouterAjax', {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            body: `produitId=${produitId}`
        })
            .then(r => r.json())
            .then(data => {
                if (!data.success) {
                    showStockAlert('Stock maximum atteint pour ce produit');
                    return;
                }

                let q = parseInt(compteurSpan.innerText) + 1;
                compteurSpan.innerText = q;

                if (q >= stockMax) {
                    btnPlus.disabled = true;
                    showStockAlert('Stock maximum atteint');

                updatePanierBadge();
            });
    });

    //  -
    btnMoins.addEventListener('click', () => {
        fetch('/Panier/ModifierQuantite', {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            body: `produitId=${produitId}&variation=-1`
        })
            .then(() => {
                let q = parseInt(compteurSpan.innerText) - 1;

                compteurSpan.innerText = q;
                // Si quantité < stock max => cacher l’alerte
                if (q < stockMax) {
                    btnPlus.disabled = false;
                    hideStockAlert(); // <-- cacher seulement ici
                }

                // Si quantité = 0 => retour bouton Ajouter
                if (q <= 0) {
                    btnQuantiteDiv.style.display = 'none';
                    btnAjouter.style.display = 'inline-block';
                }

                updatePanierBadge();
            });
    });

});




