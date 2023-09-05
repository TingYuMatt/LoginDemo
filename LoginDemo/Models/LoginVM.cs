using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoginDemo.Models
{
    public class LoginVM
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "{0}必填)")]
        [RegularExpression("[a-zA-Z0-9]", ErrorMessage = "輸入格式錯誤，請輸入[英文]或[數字]")]
        public string Acount { get; set; }
		[DisplayName("密碼")]
		[Required(ErrorMessage = "{0}必填)")]
		public string Password { get; set; }
    }
}
