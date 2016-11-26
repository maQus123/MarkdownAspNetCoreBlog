namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag {

        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public List<PostTag> PostTags { get; set; }

    }

}