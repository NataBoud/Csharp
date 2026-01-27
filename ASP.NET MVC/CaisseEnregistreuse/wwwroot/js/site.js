// site.js

// Met à jour le compteur du panier
function updatePanierBadge() {
    fetch('/Panier/Compteur')
        .then(r => r.json())
        .then(data => {
            const badge = document.getElementById('panier-badge');
            if (badge) {
                badge.innerText = data.totalItems > 0 ? data.totalItems : '';
                badge.style.display = data.totalItems > 0 ? 'inline-block' : 'none';
            }
        })
        .catch(console.error);
}

updatePanierBadge();


// Gestion universelle des alertes de stock
function getAlertesDiv() {
    let alertesDiv = document.getElementById('alertes-stock');
    if (!alertesDiv) {
        alertesDiv = document.createElement('div');
        alertesDiv.id = 'alertes-stock';
        alertesDiv.style.marginBottom = '1rem';

        const header = document.querySelector('h2');
        if (header && header.parentNode) {
            header.parentNode.insertBefore(alertesDiv, header.nextSibling);
        } else {
            document.body.prepend(alertesDiv);
        }
    }
    return alertesDiv;
}

function showStockAlert(produitId, nomProduit) {
    const alertesDiv = getAlertesDiv();
    let alertDiv = alertesDiv.querySelector(`#alert-produit-${produitId}`);

    if (!alertDiv) {
        alertDiv = document.createElement('div');
        alertDiv.id = `alert-produit-${produitId}`;
        alertDiv.className = 'alert alert-warning';
        alertDiv.innerHTML = `Quantité maximale atteinte pour <strong>${nomProduit}</strong>.`;
        alertesDiv.appendChild(alertDiv);

        void alertDiv.offsetWidth;
        alertDiv.classList.add('show');

        setTimeout(() => hideStockAlert(alertDiv), 2000);
    } else {
        alertDiv.classList.remove('show');
        void alertDiv.offsetWidth;
        alertDiv.classList.add('show');
        setTimeout(() => hideStockAlert(alertDiv), 2000);
    }
}

function hideStockAlert(alertDiv) {
    alertDiv.classList.remove('show');
    alertDiv.addEventListener('transitionend', () => {
        if (alertDiv.parentNode) alertDiv.parentNode.removeChild(alertDiv);
    }, { once: true });
}

// Gestion des boutons + du panier
document.querySelectorAll('.btn-ajouter-panier').forEach(btn => {
    btn.addEventListener('click', event => {
        event.stopPropagation();

        const produitId = btn.dataset.produitId;
        const stockDispo = parseInt(btn.dataset.stock);

        if (isNaN(stockDispo)) {
            console.warn('data-stock non défini pour le produit', produitId);
            return;
        }

        fetch('/Panier/AjouterAjax', {
            method: 'POST',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            body: `produitId=${produitId}`
        })
            .then(r => r.json())
            .then(data => {
                if (!data.success) return;

                // Met à jour le badge
                updatePanierBadge();

                // Met à jour la quantité affichée si existante
                const td = btn.closest('td');
                if (td) {
                    const quantiteSpan = td.querySelector('span');
                    if (quantiteSpan) {
                        let q = parseInt(quantiteSpan.innerText) || 0;
                        q++;
                        quantiteSpan.innerText = q;
                    }
                }

                if (data.quantiteMaxAtteinte) {
                    // Récupère le nom du produit selon la vue
                    let nomProduit = 'Produit';

                    // Si bouton dans un tr (vue Panier)
                    const tr = btn.closest('tr');
                    if (tr) {
                        const a = tr.querySelector('td a');
                        if (a) nomProduit = a.innerText.trim();
                    } else {
                        // Sinon pour Index Produit / Details Produit
                        const card = btn.closest('.card');
                        if (card) {
                            const titre = card.querySelector('.card-title');
                            if (titre) nomProduit = titre.innerText.trim();
                        }
                    }

                    showStockAlert(produitId, nomProduit);
                    btn.disabled = true;
                }
            })
            .catch(console.error);
    });
});




