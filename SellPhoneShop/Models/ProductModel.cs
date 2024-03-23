using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SellPhoneShop.Repository;
using SellPhoneShop.Repository.Validation;

namespace SellPhoneShop.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu cầu nhập tên sản phẩm!")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Status { get; set; }
        [Required, MinLength(4, ErrorMessage = "Yêu cầu nhập mô tả!")]
        public string Description { get; set; }
        public decimal Price { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một thương hiệu!")]
        public int BrandId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một danh mục!")]
        public int CategoryId { get; set; }

        public string Image {  get; set; }
        public CategoryModel Category { get; set; }
        public BrandModel Brand { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
