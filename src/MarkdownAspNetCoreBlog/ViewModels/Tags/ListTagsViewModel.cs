namespace MarkdownAspNetCoreBlog.ViewModels.Tags {

    using Models;
    using System.Collections.Generic;

    public class ListTagsViewModel {

        public ListTagsViewModel() {
            this.Tags = new List<Tag>();
        }

        public ListTagsViewModel(List<Tag> tags) {
            this.Tags = tags;
        }

        public List<Tag> Tags { get; set; }

    }

}