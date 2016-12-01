namespace MarkdownAspNetCoreBlog.ViewModels.Images {

    using Models;
    using System.Collections.Generic;

    public class ImagesViewModel {

        public ImagesViewModel() {
            this.Images = new List<Image>();
        }

        public ImagesViewModel(List<Image> images) {
            this.Images = images;
        }

        public List<Image> Images { get; set; }

    }

}