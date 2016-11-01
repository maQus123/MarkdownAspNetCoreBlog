namespace MarkdownAspNetCoreBlog.Models {

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class Post {

        public Guid Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

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