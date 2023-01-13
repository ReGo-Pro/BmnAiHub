using Core;
using Domain.Identity;

namespace WebApi.ViewModels.Core {
    public class UserViewModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalCode { get; set; }
        public string ProfilePicture { get; set; }

        public UserViewModel(User user) {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            NationalCode = user.NationalCode;
            ProfilePicture = user.ProfilePictureName;
        }
    }
}
