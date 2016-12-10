namespace MarkdownAspNetCoreBlog.Repositories.Posts {

    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PostRepository : IPostRepository {

        private readonly DataContext dataContext;

        public PostRepository(DataContext dataContext) {
            this.dataContext = dataContext;
        }

        public void Add(Post post) {
            this.dataContext.Posts.Add(post);
            return;
        }

        public void AddPostTag(PostTag postTag) {
            this.dataContext.PostTags.Add(postTag);
            return;
        }

        public List<Post> GetAll() {
            var posts = this.dataContext.Posts
                .OrderBy(p => p.CreatedAt)
                .Include(p => p.Comments)
                .Include(p => p.PostTags)
                .ThenInclude(p => p.Tag)
                .ToList();
            return posts;
        }

        public List<Post> GetAllPublished() {
            var posts = this.GetAll()
                .Where(p => p.IsPublished)
                .ToList();
            return posts;
        }

        public List<Post> GetAllPublished(Tag tag) {
            var resultPosts = new List<Post>();
            var allPosts = this.GetAllPublished();
            foreach (var post in allPosts) {
                foreach (var postTag in post.PostTags) {
                    if (postTag.Tag == tag) {
                        resultPosts.Add(post);
                        break;
                    }
                }
            }
            return resultPosts;
        }

        public Post GetById(Guid id) {
            var post = this.dataContext.Posts
                .Include(p => p.PostTags)
                .Single(p => p.Id == id);
            return post;
        }

        public Post GetByUrl(string year, string month, string slug) {
            var post = this.dataContext.Posts
                .Include(p => p.Comments)
                .Include(p => p.PostTags)
                .ThenInclude(p => p.Tag)
                .Single(p => p.Slug() == slug && p.CreatedAt.ToString("yyyyMM") == string.Concat(year, month));
            return post;
        }

        public List<PostTag> GetPostTags(Guid id) {
            var postTags = this.dataContext.PostTags
                .Where(p => p.PostId == id)
                .ToList();
            return postTags;
        }

        public bool IsPostTitleExisting(string title) {
            var exists = false;
            var postWithSameTitle = this.dataContext.Posts.Where(p => p.Title == title).Count();
            if (postWithSameTitle > 0) {
                exists = true;
            }
            return exists;
        }

        public void Remove(Post post) {
            this.dataContext.Posts.Remove(post);
            return;
        }

        public void Remove(List<PostTag> postTags) {
            this.dataContext.PostTags.RemoveRange(postTags);
            return;
        }

        public void SaveChanges() {
            this.dataContext.SaveChanges();
            return;
        }

        public void Update(Post post) {
            this.dataContext.Posts.Update(post);
            return;
        }

    }

}