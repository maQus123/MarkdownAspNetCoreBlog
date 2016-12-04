namespace MarkdownAspNetCoreBlog.ViewModels.Posts {

    using Models;
    using System.Collections.Generic;

    public class ListPostsViewModel {

        public ListPostsViewModel(List<Post> posts) {
            this.Posts = posts;
        }

        public List<Post> Posts { get; set; }

    }

}