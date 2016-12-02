namespace MarkdownAspNetCoreBlog.ViewModels.Posts {

    using Models;

    public class DetailsPostViewModel {

        public DetailsPostViewModel() {
            this.Comment = new Comment();
            this.Post = new Post();
        }

        public DetailsPostViewModel(Post post, Comment comment) {
            this.Comment = comment;
            this.Post = post;
        }

        public Post Post { get; set; }

        public Comment Comment { get; set; }

    }

}