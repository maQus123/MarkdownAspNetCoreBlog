namespace MarkdownAspNetCoreBlog.ViewModels.Images {

    using Models;

    public class DeleteImageViewModel {

        public DeleteImageViewModel(Image image) {
            this.Image = image;
        }

        public Image Image { get; set; }

    }

}