namespace MarkdownAspNetCoreBlog.Models {

    using System;

    public class PostTag {

        public PostTag() {
            this.PostId = Guid.NewGuid();
            this.Post = new Post();
            this.TagId = Guid.NewGuid();
            this.Tag = new Tag();
        }

        public PostTag(Post post, Tag tag) {
            this.Post = post;
            this.PostId = post.Id;
            this.Tag = tag;
            this.TagId = tag.Id;
        }

        public Guid PostId { get; private set; }

        public Post Post { get; private set; }

        public Guid TagId { get; private set; }

        public Tag Tag { get; private set; }

    }

}