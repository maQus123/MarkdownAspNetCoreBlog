namespace MarkdownAspNetCoreBlog.ViewModels {

    using Models;
    using System.Collections.Generic;

    public class PostsViewModel {

        public PostsViewModel(List<Post> posts) {
            this.Posts = posts;
        }

        public List<Post> Posts { get; set; }

    }

}