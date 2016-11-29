namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.Collections.Generic;

    public class Tag {

        public Tag() {
            this.Id = Guid.NewGuid();
            this.CreatedAt = new DateTimeOffset(DateTime.UtcNow);
            this.PostTags = new List<PostTag>();
        }

        public Guid Id { get; private set; }

        public string Title { get; set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public List<PostTag> PostTags { get; private set; }

    }

}