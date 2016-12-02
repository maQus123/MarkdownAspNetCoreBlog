namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment {

        public Comment() {
            this.Id = Guid.NewGuid();
            this.CreatedAt = new DateTimeOffset(DateTime.UtcNow);
        }

        public Comment(Guid id, DateTimeOffset createdAt) {
            this.Id = id;
            this.CreatedAt = createdAt;
        }

        public Guid Id { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

    }

}