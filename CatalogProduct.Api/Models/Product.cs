using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogProduct.Api.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }

        public float Stock { get; set; }

        public DateTime CreateDate { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}