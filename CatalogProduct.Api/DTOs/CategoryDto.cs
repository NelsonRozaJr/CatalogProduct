using System.Collections.Generic;

namespace CatalogProduct.Api.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<ProductDto> Products { get; set; }
    }
}