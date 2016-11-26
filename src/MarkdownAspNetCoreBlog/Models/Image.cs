namespace MarkdownAspNetCoreBlog.Models {

    using System;

    public class Image {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset UploadedAt { get; set; }

    }

}