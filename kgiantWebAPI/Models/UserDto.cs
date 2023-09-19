using System.ComponentModel.DataAnnotations;

namespace kgiantWebAPI.Models
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "성을 입력해 주세요.")]
        public string LastName { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        public string Phone { get; set; } = "";

        [Required]
        [MinLength(5, ErrorMessage = "5글자 이상 작성해 주세요.")]
        [MaxLength(1000, ErrorMessage = "1000 글자내로 입력해 주세요.")]
        public string Address { get; set; } = "";
    }
}
