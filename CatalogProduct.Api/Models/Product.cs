using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogProduct.Api.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 80 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(300, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
        public string Description { get; set; }

        [Required]
        // O parâmetro {0} é reservado para o nome da propriedade, os outros são parâmetros da restrição
        [Range(1, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
        public decimal UnitPrice { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string ImageUrl { get; set; }

        public float Stock { get; set; }

        public DateTime CreateDate { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}