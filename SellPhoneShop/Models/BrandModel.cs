using System.ComponentModel.DataAnnotations;
using SellPhoneShop.Repository;

namespace SellPhoneShop.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Bạn chưa nhập tên!")]
        public string Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Bạn chưa nhập mô tả!")]
        public string Description { get; set; }
        
        public string Slug { get; set; }
        public int Status { get; set; }
    }
}
