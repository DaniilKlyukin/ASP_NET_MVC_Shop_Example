using System.ComponentModel.DataAnnotations;

namespace ASP_NET_MVC_Shop_Example.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Название товара")]
        [Required(ErrorMessage = "Не указано имя товара")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Display(Name = "Цена, руб")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Не указана цена")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть не менее 0,01")]
        public double Price { get; set; }

        [Display(Name = "Описание")]
        [StringLength(500)]
        public string? Description { get; set; }
    }
}