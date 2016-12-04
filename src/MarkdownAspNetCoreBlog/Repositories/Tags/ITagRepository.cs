namespace MarkdownAspNetCoreBlog.Repositories.Tags {

    using Models;
    using System;
    using System.Collections.Generic;

    public interface ITagRepository {

        void Add(Tag tag);
        List<Tag> GetAll();
        Tag GetById(Guid id);
        void Remove(Tag tag);
        void SaveChanges();
        void Update(Tag tag);

    }

}