namespace CatalogProduct.Api.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}