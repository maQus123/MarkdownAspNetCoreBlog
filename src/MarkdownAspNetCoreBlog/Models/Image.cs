namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.ComponentModel.DataAnnotations;

    public class Image {

        public Image() {
            this.Id = Guid.NewGuid();
            this.UploadedAt = new DateTimeOffset(DateTime.UtcNow);
        }

        public Image(Image image) {
            this.Id = image.Id;
            this.FilePath = image.FilePath;
            this.Title = image.Title;
            this.UploadedAt = image.UploadedAt;
        }

        public Guid Id { get; private set; }

        public string FilePath { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTimeOffset UploadedAt { get; private set; }

    }

}