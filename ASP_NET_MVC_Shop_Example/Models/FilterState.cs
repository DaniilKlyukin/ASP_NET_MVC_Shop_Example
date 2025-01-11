using System.ComponentModel.DataAnnotations;

namespace ASP_NET_MVC_Shop_Example.Models
{
    public class FilterState : IValidatableObject
    {
        public FilterState(string? productName, double? minPrice, double? maxPrice)
        {
            ProductName = productName;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public FilterState()
        {

        }

        [Display(Name = "Название товара")]
        [StringLength(100)]
        public string? ProductName { get; set; }

        [Display(Name = "Минимальная цена, руб")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть не менее 0,01")]
        public double? MinPrice { get; set; }

        [Display(Name = "Максимальная цена, руб")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть не менее 0,01")]
        public double? MaxPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinPrice > MaxPrice)
            {
                yield return new ValidationResult("Максмальная цена не может быть меньше минимальной");
            }
        }
    }
}