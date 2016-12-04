namespace MarkdownAspNetCoreBlog.ViewModels.Tags {

    using Models;

    public class UpdateTagViewModel {

        public UpdateTagViewModel(Tag tag) {
            this.Tag = tag;
        }

        public Tag Tag { get; set; }

    }

}