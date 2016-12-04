namespace MarkdownAspNetCoreBlog.ViewModels.Posts {

    using Models;

    public class DeletePostViewModel {

        public DeletePostViewModel(Post post) {
            this.Post = post;
        }

        public Post Post { get; private set; }

    }

}