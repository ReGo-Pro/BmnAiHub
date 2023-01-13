using Domain.Core;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Identity {
    public class User : IdentityUser {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? NationalCode { get; set; }

        public string? ProfilePictureName { get; set; }

        public virtual List<BlogPost> PublishedPosts { get; set; }
        public virtual List<BlogPost> SupervisedPosts { get; set; }
        public virtual List<Education> Educations { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
