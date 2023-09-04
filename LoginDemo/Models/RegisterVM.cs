using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoginDemo.Models
{
    public class RegisterVM
    {

        [DisplayName("姓名")]
        [Required(ErrorMessage = "{0}必填)")]
        public string Name { get; set; }
        [DisplayName("電子郵件")]
        [MaxLength(50, ErrorMessage = "{0}長度不可大於{1}")]
        [Required(ErrorMessage = "{0}必填")]
        [EmailAddress(ErrorMessage = "帳號格式錯誤")]
        public string Email { get; set; }

        [DisplayName("密碼")]
        [MaxLength(20, ErrorMessage = "{0}長度不可大於{1}")]
        [Required(ErrorMessage = "{0}必填")]
        [RegularExpression("[a-zA-Z0-9]{8,}", ErrorMessage = "輸入格式錯誤,請輸入8至20字元[英文]或[數字]")]
        public string Password { get; set; }

        public DateTime Birthday { get; set; }
        [DisplayName("檔名")]
        [Required(ErrorMessage = "{0}必填)")]
        public string FileName { get; set; }
        [DisplayName("單位")]
        [Required(ErrorMessage = "{0}必填)")]
        public string Title { get; set; }
        [DisplayName("帳號")]
        [Required(ErrorMessage = "{0}必填)")]
        [RegularExpression("[a-zA-Z0-9]",ErrorMessage ="輸入格式錯誤，請輸入[英文]或[數字]")]
        public string Acount { get; set; }

    }
}
