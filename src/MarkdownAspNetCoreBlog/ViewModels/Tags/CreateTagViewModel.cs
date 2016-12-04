namespace MarkdownAspNetCoreBlog.ViewModels.Tags {

    using Models;

    public class CreateTagViewModel {

        public CreateTagViewModel() {
            this.NewTag = new Tag();
        }

        public Tag NewTag { get; set; }

    }

}