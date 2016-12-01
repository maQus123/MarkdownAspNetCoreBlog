namespace MarkdownAspNetCoreBlog.ViewModels.Tags {

    using Models;

    public class TagViewModel {

        public TagViewModel() {
            this.Tag = new Tag();
        }

        public TagViewModel(Tag tag) {
            this.Tag = tag;
        }

        public Tag Tag { get; set; }

    }

}