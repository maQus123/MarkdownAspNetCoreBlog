namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class Tag {

        public Tag() {
            this.Id = Guid.NewGuid();
            this.CreatedAt = new DateTimeOffset(DateTime.UtcNow);
            this.PostTags = new List<PostTag>();
        }

        public Tag(Tag tag) {
            this.Id = tag.Id;
            this.Title = tag.Title;
            this.CreatedAt = tag.CreatedAt;
            this.PostTags = tag.PostTags;
        }

        public void UpdateFrom(Tag tag) {
            this.Title = tag.Title;
            return;
        }
        public Guid Id { get; private set; }

        [Required]
        public string Title { get; set; }

        public DateTimeOffset CreatedAt { get; private set; }

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