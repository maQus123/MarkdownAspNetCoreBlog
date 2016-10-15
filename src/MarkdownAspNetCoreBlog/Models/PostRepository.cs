namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.Collections.Generic;

    public class PostRepository {

        static List<Post> posts = new List<Post>();

        public PostRepository() {
            //nothing to do
        }

        public void Add(Post post) {
            PostRepository.posts.Add(post);
            return;
        }

        public Post Find(string year, string month, string slug) {
            Post result = null;
            var date = string.Concat(year, month);
            foreach (Post post in PostRepository.posts) {
                if (post.CreatedAt.ToString("yyyyMM") == date && post.Slug() == slug) {
                    result = post;
                    break;
                }
            }
            return result;
        }

        public IEnumerable<Post> GetAll() {
            return PostRepository.posts;
        }

    }

}