using AnimalProject.Data;
using AnimalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalProject.Controllers
{
    public class CatalogController : Controller
    {
        private AnimalContext context;
        public CatalogController(AnimalContext animalContext)
        {
            context = animalContext;
        }
        public IActionResult CatalogPage(string? categoryName)
        {
            var categories = context.Categories.ToList();
            ViewBag.Categories = categories;

            List<Animal> animals;

            if (string.IsNullOrEmpty(categoryName) || categoryName == "All")
            {
                animals = context.Animals.Include(a => a.Category).ToList();
            }
            else
            {
                animals = context.Animals
                                 .Include(a => a.Category)
                                 .Where(a => a.Category.Name == categoryName)
                                 .ToList();
            }

            return View(animals);
        }
    }
}
