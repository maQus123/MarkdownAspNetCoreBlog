namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag {

        public Tag() {
            this.Id = Guid.NewGuid();
            this.CreatedAt = new DateTimeOffset(DateTime.UtcNow);
            this.PostTags = new List<PostTag>();
        }

        public Tag(Tag tag) {
            this.Id = tag.Id;
            this.Title = tag.Title;
            this.CreatedAt = tag.CreatedAt;
            this.PostTags = tag.PostTags;
        }

        public void UpdateFrom(Tag tag) {
            this.Title = tag.Title;
            return;
        }
        public Guid Id { get; private set; }

        [Required]
        public string Title { get; set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public List<PostTag> PostTags { get; private set; }

    }

}