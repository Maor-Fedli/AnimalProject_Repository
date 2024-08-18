using AnimalProject.Data;
using Microsoft.AspNetCore.Mvc;
using AnimalProject.Models;

namespace AnimalProject.Controllers
{
    public class MainController : Controller
    {
        private AnimalContext context;
        public MainController(AnimalContext animalContext)
        {
            context = animalContext;
        }

        public IActionResult Home()
        {
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Animals = context.Animals.ToList();
            var animals = AnimalsMainPage();
            return View(animals);
        }
        public List<Animal> AnimalsMainPage()
        {
            var animalsWithTopComments = context.Animals
                .OrderByDescending(a => a.Comments!.Count)
                .Take(2)
                .ToList();

            return animalsWithTopComments;
        }


    }
}
