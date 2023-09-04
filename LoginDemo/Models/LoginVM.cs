using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoginDemo.Models
{
    public class LoginVM
    {
        [DisplayName("姓名")]
        [Required(ErrorMessage ="{0}必填)")]
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
