using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.Identity {
    public class LoginViewModel {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(64)]
        public string Password { get; set; }
    }
}
