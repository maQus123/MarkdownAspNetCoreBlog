namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.Collections.Generic;

    public class PostRepository {

        static List<Post> posts;

        public PostRepository() {
            PostRepository.posts = new List<Post>();
        }

        public void Add(Post post) {
            post.CreatedAt = new DateTimeOffset(DateTime.UtcNow);
            post.Id = Guid.NewGuid();
            PostRepository.posts.Add(post);
            return;
        }

        public Post Find(Guid id) {
            Post existingPost = null;
            foreach (var post in PostRepository.posts) {
                if (post.Id == id) {
                    existingPost = post;
                    break;
                }
            }
            return existingPost;
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

        public void Update(Guid id, Post post) {
            foreach (var existingPost in PostRepository.posts) {
                if (id == existingPost.Id) {
                    existingPost.Title = post.Title;
                    existingPost.Content = post.Content;
                    break;
                }
            }
            return;
        }
    }

}