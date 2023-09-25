using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace kgiantWebAPI.Models
{
    public class LoginDto
    {
        [Required(ErrorMessage = "아이디를 입력해 주세요.")]
        public string Id { get; set; } = "";

        [Required(ErrorMessage = "비밀번호를 입력해 주세요."), PasswordPropertyText]
        public string password { get; set; } = "";
    }
}
