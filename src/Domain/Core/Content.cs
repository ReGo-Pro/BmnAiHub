using Core;
using Domain.Identity;

namespace Domain.Core {
    public abstract class Content {
        public int ID { get; set; }

        public virtual User Author { get; set; }
        public string AuthorID { get; set; }

        public string Slug { get; set; }
        public string Title { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? publishedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User SupervisedBy { get; set; }
        public string SupervisedByID { get; set; }

        public string TextContent { get; set; }
        public int ViewCount { get; set; }        
    }
}
