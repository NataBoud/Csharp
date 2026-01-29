using BlazorDemo.Shared.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorDemo.Client.Services;


public class ProduitServiceClient : IProduitServiceClient
{
    private readonly HttpClient _http;
    private const string BaseUrl = "api/produits";

    public ProduitServiceClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Produit>> ObtenirTousAsync()
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<Produit>>(BaseUrl);
            return result ?? new List<Produit>();
        }
        catch
        {
            return new List<Produit>();
        }
    }

    Task<Produit?> IProduitServiceClient.ObtenirParIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    async Task<List<Produit>> IProduitServiceClient.RechercherAsync(string terme)
    {
        if (string.IsNullOrWhiteSpace(terme))
            return await ObtenirTousAsync(); // Si rien à rechercher, retourne tous les produits

        try
        {
            // Encode le terme pour éviter les problèmes avec les caractères spéciaux
            var url = $"api/produits/recherche?terme={Uri.EscapeDataString(terme)}";
            var result = await _http.GetFromJsonAsync<List<Produit>>(url);
            return result ?? new List<Produit>();
        }
        catch
        {
            return new List<Produit>();
        }
    }

    Task<List<Produit>> IProduitServiceClient.ObtenirParCategorieAsync(string categorie)
    {
        //  appel l’API côté serveur pour obtenir les produits filtrés
        return _http.GetFromJsonAsync<List<Produit>>($"api/produits/categorie/{categorie}")!;
    }

    async Task<Produit?> IProduitServiceClient.AjouterAsync(ProduitDto dto)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(BaseUrl, dto);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Produit>();
        }
        catch
        {
            return null;
        }
    }

    async Task<Produit?> IProduitServiceClient.ModifierAsync(int id, ProduitDto dto)
    {
        try
        {
            // Appel PUT /api/produits/{id} avec le DTO
            var response = await _http.PutAsJsonAsync($"{BaseUrl}/{id}", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            // Retourne le produit modifié depuis la réponse JSON
            return await response.Content.ReadFromJsonAsync<Produit>();
        }
        catch
        {
            return null;
        }
    }

    async Task<bool> IProduitServiceClient.SupprimerAsync(int id)
    {
        try
        {
            var response = await _http.DeleteAsync($"{BaseUrl}/{id}");

            // Retourne vrai si succès (status 2xx)
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    Task<List<string>> IProduitServiceClient.ObtenirCategoriesAsync()
    {
        throw new NotImplementedException();
    }
}
