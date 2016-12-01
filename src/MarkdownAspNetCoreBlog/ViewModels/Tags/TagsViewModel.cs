namespace MarkdownAspNetCoreBlog.ViewModels.Tags {

    using Models;
    using System.Collections.Generic;

    public class TagsViewModel {

        public TagsViewModel() {
            this.Tags = new List<Tag>();
        }

        public TagsViewModel(List<Tag> tags) {
            this.Tags = tags;
        }

        public List<Tag> Tags { get; set; }

    }

}