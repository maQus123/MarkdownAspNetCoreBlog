namespace MarkdownAspNetCoreBlog.ViewModels.Images {

    using Models;

    public class ImageViewModel {

        public ImageViewModel() {
            this.Image = new Image();
        }

        public ImageViewModel(Image image) {
            this.Image = image;
        }

        public Image Image { get; set; }

    }

}