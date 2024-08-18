using AnimalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace AnimalProject.Models
{
    public class Animal
    {
        [Key]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Name must contain only letters")]
        public string? Name { get; set; }//AnimalName

        [Range(0,120, ErrorMessage = "Age must be a positive number")]
        public int Age { get; set; }
        public string? PictureName { get; set; }
        public string? Description { get; set; }// תיאור
        public virtual ICollection<Comment>? Comments { get; set; }
        public int CategoryId { get; set; }


        [Required(ErrorMessage = "Category is required")]
        public virtual Category? Category { get; set; }

    }
}

