using System.ComponentModel.DataAnnotations;

namespace SellPhoneShop.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Yêu cầu nhập Username")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Yêu cầu nhập Email"),EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password),Required(ErrorMessage = "Yêu cầu nhập Password")]
		public string Password { get; set; }
	}
}
