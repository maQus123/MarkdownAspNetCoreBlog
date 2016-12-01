namespace MarkdownAspNetCoreBlog.ViewModels.Posts {

    using Models;
    using System.Collections.Generic;

    public class PostsViewModel {

        public PostsViewModel() {
            this.Posts = new List<Post>();
        }

        public PostsViewModel(List<Post> posts) {
            this.Posts = posts;
        }

        public List<Post> Posts { get; set; }

    }

}