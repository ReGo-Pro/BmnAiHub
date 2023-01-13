using data;
using data.Repositories;
using Data.Interfaces;
using Domain.Core;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories {
    public class UserRepository : Repository<User, string>, IUserRepositroy {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<List<Education>> GetUserEducationAsync(string userId) {
            return await Context.Educations.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task UpdateProfilePictureNameAsync(string userId, string profilePictureName) {
            var user = await FindByIdAsync(userId);
            user.ProfilePictureName = profilePictureName;
            await Context.SaveChangesAsync();
        }

        protected override AppDbContext Context => base.Context as AppDbContext;
    }
}
