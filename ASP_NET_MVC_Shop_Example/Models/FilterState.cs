using System.ComponentModel.DataAnnotations;

namespace ASP_NET_MVC_Shop_Example.Models
{
    public class FilterState
    {
        public FilterState(double? minPrice, double? maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public FilterState()
        {

        }

        [Display(Name = "Минимальная цена, руб")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть не менее 0,01")]
        public double? MinPrice { get; set; }

        [Display(Name = "Максимальная цена, руб")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть не менее 0,01")]
        public double? MaxPrice { get; set; }
    }
}