using AnimalProject.Models;
using System.ComponentModel.DataAnnotations;

namespace AnimalProject.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public int AnimalId { get; set; }
        public string? comment { get; set; }
        public virtual Animal? Animal { get; set; } // קשר רבים-לאחד ל-Animal

    }
}
