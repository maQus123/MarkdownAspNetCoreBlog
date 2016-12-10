namespace MarkdownAspNetCoreBlog.Repositories.Posts {

    using Models;
    using System;
    using System.Collections.Generic;

    public interface IPostRepository {

        void Add(Post post);
        void AddPostTag(PostTag postTag);
        List<Post> GetAll();
        List<Post> GetAllPublished();
        List<Post> GetAllPublished(Tag tag);
        Post GetById(Guid id);
        Post GetByUrl(string year, string month, string slug);
        List<PostTag> GetPostTags(Guid id);
        bool IsPostTitleExisting(string title);
        void Remove(List<PostTag> postTags);
        void Remove(Post post);
        void SaveChanges();
        void Update(Post post);

    }

}