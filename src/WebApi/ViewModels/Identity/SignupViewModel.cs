using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.Identity {
    public class SignupViewModel {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [MaxLength(128)]
        public string FirstName { get; set; }

        [MaxLength(128)]
        public string LastName { get; set; }
    }
}
