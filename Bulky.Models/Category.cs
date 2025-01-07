using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [MaxLength(30)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
