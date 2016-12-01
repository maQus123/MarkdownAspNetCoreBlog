namespace MarkdownAspNetCoreBlog.Models {

    using System;

    public class Image {

        public Image() {
            this.Id = Guid.NewGuid();
            this.UploadedAt = new DateTimeOffset(DateTime.UtcNow);
        }

        public Image(Image image) {
            this.Id = image.Id;
            this.FolderName = image.FolderName;
            this.Name = image.Name;
            this.UploadedAt = image.UploadedAt;
        }

        public void UpdateFrom(Image image) {
            this.Name = image.Name;
            return;
        }

        public Guid Id { get; private set; }

        public string FolderName { get; set; }

        public string Name { get; set; }

        public DateTimeOffset UploadedAt { get; private set; }

    }

}