namespace MarkdownAspNetCoreBlog.Repositories.Posts {

    using Models;
    using System;
    using System.Collections.Generic;

    public interface IPostRepository {

        void Add(Post post);
        void AddPostTagRelationship(PostTag postTag);
        List<Post> GetAll();
        List<Post> GetAllPublished();
        Post GetById(Guid id);
        Post GetByUrl(string year, string month, string slug);
        List<PostTag> GetPostTagRelationshipsByPostId(Guid id);
        void Remove(List<PostTag> postTags);
        void Remove(Post post);
        void SaveChanges();
        void Update(Post post);

    }

}