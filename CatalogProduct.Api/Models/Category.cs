using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatalogProduct.Api.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}