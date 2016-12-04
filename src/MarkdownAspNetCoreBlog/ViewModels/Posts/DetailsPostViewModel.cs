namespace MarkdownAspNetCoreBlog.ViewModels.Posts {

    using Models;

    public class DetailsPostViewModel {

        public DetailsPostViewModel() {
            this.NewComment = new Comment();
        }

        public DetailsPostViewModel(Post post) {
            this.Post = post;
        }

        public Post Post { get; set; }

        public Comment NewComment { get; set; }

    }

}