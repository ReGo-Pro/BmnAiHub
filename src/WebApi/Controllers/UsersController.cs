using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.IdentityModel.Tokens.Jwt;
using webapi.Controllers;
using WebApi.ViewModels.Core;

namespace WebApi.Controllers {
    [Authorize]
    public class UsersController : ApiController {
        private readonly UserService _userService;
        private readonly StaticFilesManager _fileManager;

        public UsersController(UserService userService, StaticFilesManager fileManager) {
            _userService = userService;
            _fileManager = fileManager;
        }

        [HttpGet("Current")]
        public async Task<IActionResult> GetCurrentUserInfo() {
            try {
                var user = await _userService.GetUserInfo(CurrentUserId);
                return Ok(new UserViewModel(user));
            }
            catch (Exception) {
                return InternalServerError();
            }
        }

        [HttpGet("Current/Education")]
        public async Task<IActionResult> GetCurrentUserEducation() {
            try {
                var educationList = await _userService.GetUserEducation(CurrentUserId);
                return Ok(educationList.Select(e => new EducationViewModel(e)));
            }
            catch (Exception) {
                return InternalServerError();
            }
        }

        [HttpPost("Current/ProfilePicture")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] IFormFile file) {
            try {
                // TODO: further validations are required
                var fileName = await _fileManager.SaveUserProfilePicture(file);
                await _userService.UpdateProfilePictureAsync(CurrentUserId, fileName);
                return Ok(fileName);
            }
            catch (Exception) {
                return InternalServerError();
            }
        }

        private string CurrentUserId => User.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
    }
}
