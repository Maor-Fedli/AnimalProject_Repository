using AnimalProject.Data;
using AnimalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalProject.Controllers
{
    public class AnimalController : Controller
    {
        private AnimalContext context;
        public AnimalController(AnimalContext animalContext)
        {
            context = animalContext;
        }
        public IActionResult AnimalPage(int animalId)
        {
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Animals = context.Animals.ToList();
            ViewBag.Comments = context.Animals
            .Include(a => a.Comments)
            .Where(a => a.AnimalId == animalId)
            .ToList();

            var selectedAnimal = context.Animals.FirstOrDefault(c => c.AnimalId == animalId);


            return View(selectedAnimal);
        }
        public IActionResult AddComment(int animalId, string commentText)
        {
            var newComment = new Comment
            {
                AnimalId = animalId,
                comment = commentText,
            };

            context.Comments.Add(newComment);
            context.SaveChanges();

            return RedirectToAction("AnimalPage", "Animal", new { animalId = animalId });

        }

    }
}
