using MarkdownAspNetCoreBlog.Models;

namespace MarkdownAspNetCoreBlog.ViewModels.Images {

    public class CreateImageViewModel {

        public CreateImageViewModel() {
            this.Image = new Image();
        }

        public Image Image { get; set; }

    }

}