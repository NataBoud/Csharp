// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

fetch('/Panier/Compteur')
    .then(r => r.json())
    .then(data => {
        const badge = document.getElementById('panier-badge');
        if (badge && data.totalItems > 0) {
            badge.innerText = data.totalItems;
            badge.style.display = 'inline-block';
        }
    });


document.querySelectorAll('.btn-ajouter-panier').forEach(btn => {

    btn.addEventListener('click', (event) => {

        event.stopPropagation(); // empêche le clic de remonter au parent <a>

        const produitId = btn.dataset.produitId;

        fetch('/Panier/AjouterAjax', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: `produitId=${produitId}`
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {

                    alert(`Produit ajouté au panier. Total articles: ${data.totalItems}`);

                    const badge = document.getElementById('panier-badge');
                    if (badge) {
                        badge.innerText = data.totalItems;
                        badge.style.display = 'inline-block';
                    }
                }
            })
            .catch(error => console.error(error));
    });
});

