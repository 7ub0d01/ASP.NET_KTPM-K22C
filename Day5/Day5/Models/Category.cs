using System.ComponentModel.DataAnnotations;

namespace Day5.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

        // Navigation property- thuoc tinh dieu huong
        public List<Product> Products { get; set; }

    }
}
