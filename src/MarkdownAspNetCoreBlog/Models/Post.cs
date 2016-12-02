namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class Post {

        public Post() {
            this.Id = Guid.NewGuid();
            this.CreatedAt = new DateTimeOffset(DateTime.UtcNow);
            this.PostTags = new List<PostTag>();
            this.Comments = new List<Comment>();
        }

        public Post(Post post) {
            this.Id = post.Id;
            this.Title = post.Title;
            this.Content = post.Content;
            this.IsPublished = post.IsPublished;
            this.CreatedAt = post.CreatedAt;
            this.PostTags = post.PostTags;
            this.Comments = post.Comments;
        }

        public void UpdateFrom(Post post) {
            this.Title = post.Title;
            this.Content = post.Content;
            this.IsPublished = post.IsPublished;
            return;
        }

        public Guid Id { get; private set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public bool IsPublished { get; set; }

        public List<PostTag> PostTags { get; private set; }

        public List<Comment> Comments { get; private set; }

        public string Slug() {
            string slug = this.Title.ToLower();
            slug = Regex.Replace(slug, "ö", "oe");
            slug = Regex.Replace(slug, "ä", "ae");
            slug = Regex.Replace(slug, "ü", "ue");
            slug = Regex.Replace(slug, "ß", "ss");
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            slug = slug.Substring(0, slug.Length <= 45 ? slug.Length : 45).Trim();
            slug = Regex.Replace(slug, @"\s", "-");
            return slug;
        }

    }

}