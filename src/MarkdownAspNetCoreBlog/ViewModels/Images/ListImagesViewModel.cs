namespace MarkdownAspNetCoreBlog.ViewModels.Images {

    using Models;
    using System.Collections.Generic;

    public class ListImagesViewModel {

        public ListImagesViewModel() {
            this.Images = new List<Image>();
        }

        public ListImagesViewModel(List<Image> images) {
            this.Images = images;
        }

        public List<Image> Images { get; set; }

    }

}