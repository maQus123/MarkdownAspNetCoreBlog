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

        public void AddPostTagRelationship(PostTag postTag) {
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

        public List<PostTag> GetPostTagRelationshipsByPostId(Guid id) {
            var postTags = this.dataContext.PostTags
                .Where(p => p.PostId == id)
                .ToList();
            return postTags;
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