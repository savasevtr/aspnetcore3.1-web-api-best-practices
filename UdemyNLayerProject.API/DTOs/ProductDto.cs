using System.ComponentModel.DataAnnotations;

namespace UdemyNLayerProject.API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Display(Name = "Ürün adı")]
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string Name { get; set; }

        [Display(Name = "Stok")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} alanı 1'den büyük olmalıdır")]
        public int Stock { get; set; }

        [Display(Name = "Fiyat")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} alanı 1'den büyük olmalıdır")]
        public decimal Price { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public int CategoryId { get; set; }
    }
}