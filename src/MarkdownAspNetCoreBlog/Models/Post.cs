namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Post {

        public Post() {
            this.Id = Guid.NewGuid();
            this.CreatedAt = new DateTimeOffset(DateTime.UtcNow);
            this.PostTags = new List<PostTag>();
        }

        public Guid Id { get; private set; }
                
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public bool IsPublished { get; set; }

        public List<PostTag> PostTags { get; private set; }

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