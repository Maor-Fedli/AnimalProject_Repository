using AnimalProject.Data;
using AnimalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalProject.Controllers
{
    public class AdminController : Controller
    {
        private AnimalContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdminController(AnimalContext animalContext, IWebHostEnvironment webHostEnvironment)
        {
            context = animalContext;
            this.webHostEnvironment = webHostEnvironment;// העלאת קבצים
        }
        public IActionResult AdminPage(string? categoryName)
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
        public IActionResult DeleteAnimal(int animalId)
        {
           
            var animal = context.Animals.Find(animalId);

            if (animal != null)
            {
                context.Animals.Remove(animal);
                context.SaveChanges();
            }
            return RedirectToAction("AdminPage", "Admin");
        }
        public IActionResult EditAnimal(int animalId)
        {
            var animal = context.Animals.FirstOrDefault(a => a.AnimalId == animalId);
            return View(animal);
        }
        public IActionResult Edit(int animalId, string name, string category, int age, string description)
        {
            var editAnimal = context.Animals.Include(a => a.Category).FirstOrDefault(a => a.AnimalId == animalId);

            if (editAnimal == null)
            {
                return NotFound(); 
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                editAnimal.Name = name;
                editAnimal.PictureName = "picture"+name+"1.jpg";
            }
            if (age > 0)
            {
                editAnimal.Age = age;
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                editAnimal.Description = description;
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                editAnimal.Category.Name = category;
            }
            context.SaveChanges();
            return RedirectToAction("EditAnimal", new { animalId = animalId });
        }
        public IActionResult AddAnimalPage()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddAnimal(string Name, string category, int Age, string Description, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                var cate = new Category();
                cate.Name = category;
                var newAnimal = new Animal
                {
                    Name = Name,
                    Category = cate,
                    Age = Age,
                    Description = Description,
                    PictureName = Image?.FileName
                };

               
                context.Animals.Add(newAnimal);
                await context.SaveChangesAsync();

                if (Image != null && Image.Length > 0)
                {
                    // יצירת תיקייה חדשה עם מזהה החיה
                    var animalDirectory = Path.Combine(webHostEnvironment.WebRootPath, "Pictures", newAnimal.AnimalId.ToString());
                    if (!Directory.Exists(animalDirectory))
                    {
                        Directory.CreateDirectory(animalDirectory);
                    }

                    // שמירה בקובץ
                    var filePath = Path.Combine(animalDirectory, $"picture{newAnimal.AnimalId}.jpg");
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }
                }

                return RedirectToAction("AdminPage"); 
            }

            return View();
        }

    }
}
