using CaisseEnregistreuse.Models;
using CaisseEnregistreuse.Services;
using CaisseEnregistreuse.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class CategorieController : Controller
{
    private readonly ICategorieService _categorieService;

    public CategorieController(ICategorieService categorieService)
    {
        _categorieService = categorieService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categorieService.GetAllCategoriesAsync();
       
        return View(categories);
    }

    public async Task<IActionResult> Details(int id)
    {
        var categorie = await _categorieService.GetCategorieByIdAsync(id);
        if (categorie == null) return NotFound();
        return View(categorie);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Categorie categorie)
    {
        if (!ModelState.IsValid) return View(categorie);

        try
        {
            await _categorieService.AddCategorieAsync(categorie);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(categorie);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var categorie = await _categorieService.GetCategorieByIdAsync(id);
        if (categorie == null) return NotFound();
        return View(categorie);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Categorie categorie)
    {
        if (!ModelState.IsValid) return View(categorie);

        try
        {
            await _categorieService.UpdateCategorieAsync(categorie);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(categorie);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _categorieService.DeleteCategorieAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
