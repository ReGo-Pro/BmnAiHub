using Core;
using Microsoft.AspNetCore.Http;

namespace Service {
    public class StaticFilesManager {
        public string wwwroot { get; set; }

        public StaticFilesManager(string webRootPath) {
            wwwroot = webRootPath;
        }

        public string GetUserProfilePictureUri(string fileName) {
            return Path.Combine(AppSettings.Path.ProfilePictures, fileName);
        }

        public string GetPostBannerImageUri(string fileName) {
            return Path.Combine(AppSettings.Path.PostBanners, fileName);
        }

        public async Task<string> SaveUserProfilePicture(IFormFile file) {
            var fileNameGuid = GetFileNameGuid();
            var fileName = Path.Combine(wwwroot, "media", AppSettings.Path.ProfilePictures, fileNameGuid);
            using (var stream = File.Create(fileName)) {
                await file.CopyToAsync(stream);
            }

            return fileNameGuid;
        }

        public string SaveBannerImage(Stream image) {
            throw new NotImplementedException();
        }

        private string GetFileNameGuid() {
            var guid = Guid.NewGuid().ToString();
            return $"{guid}.png";
        }
    }
}
