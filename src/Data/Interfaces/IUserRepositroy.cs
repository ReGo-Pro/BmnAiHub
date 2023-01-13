using data.Interfaces;
using Domain.Core;
using Domain.Identity;

namespace Data.Interfaces {
    public interface IUserRepositroy : IRepository<User, string> {
        Task<List<Education>> GetUserEducationAsync(string userId);
        Task UpdateProfilePictureNameAsync(string userId, string profilePictureName);
    }
}
