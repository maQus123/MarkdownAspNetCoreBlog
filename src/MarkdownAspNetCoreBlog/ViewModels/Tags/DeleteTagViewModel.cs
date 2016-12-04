namespace MarkdownAspNetCoreBlog.ViewModels.Tags {

    using Models;

    public class DeleteTagViewModel {

        public DeleteTagViewModel(Tag tag) {
            this.Tag = tag;
        }

        public Tag Tag { get; set; }

    }

}