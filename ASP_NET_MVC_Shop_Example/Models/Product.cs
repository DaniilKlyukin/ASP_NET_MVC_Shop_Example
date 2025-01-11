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

        [Required(ErrorMessage = "Не указана цена")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть не менее 0,01")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
