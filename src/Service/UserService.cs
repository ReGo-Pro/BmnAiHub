using Data.Interfaces;
using Domain.Core;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service {
    public class UserService {
        private readonly IUserRepositroy _userRepositroy;

        public UserService(IUserRepositroy userRepositroy) {
            _userRepositroy = userRepositroy;
        }

        public async Task<User> GetUserInfo(string userId) {
            return await _userRepositroy.FindByIdAsync(userId);
        }

        public async Task<List<Education>> GetUserEducation(string userId) {
            return await _userRepositroy.GetUserEducationAsync(userId);
        }

        public async Task<User> FindById(string userId) {
            return await _userRepositroy.FindByIdAsync(userId);
        }

        public async Task UpdateProfilePictureAsync(string userId, string profilePictureName) {
            await _userRepositroy.UpdateProfilePictureNameAsync(userId, profilePictureName);
        }
    }
}
