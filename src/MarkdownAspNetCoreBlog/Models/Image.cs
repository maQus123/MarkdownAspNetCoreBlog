namespace MarkdownAspNetCoreBlog.Models {

    using System;

    public class Image {

        public Image() {
            this.Id = Guid.NewGuid();
            this.UploadedAt = new DateTimeOffset(DateTime.UtcNow);
        }

        public Guid Id { get; private set; }

        public string Name { get; set; }

        public DateTimeOffset UploadedAt { get; private set; }

    }

}