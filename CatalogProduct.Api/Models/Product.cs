using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CatalogProduct.Api.Validations;

namespace CatalogProduct.Api.Models
{
    public class Product : IValidatableObject
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [FirstLetterUpperCase]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 80 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(300, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(this.Name))
            {
                string firstLetter = this.Name[0].ToString();
                if (firstLetter != firstLetter.ToUpper())
                {
                    yield return new ValidationResult("A primeira letra do nome do produto deve ser maiúscula.", 
                        new[] { nameof(this.Name) });
                }
            }

            if (this.Stock < 0)
            {
                yield return new ValidationResult("O estoque deve ser maior ou igual a zero.", 
                    new[] { nameof(this.Stock) });
            }
        }
    }
}