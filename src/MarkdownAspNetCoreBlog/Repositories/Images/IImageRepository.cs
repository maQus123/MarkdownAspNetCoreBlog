namespace MarkdownAspNetCoreBlog.Repositories.Images {

    using Models;
    using System;
    using System.Collections.Generic;

    public interface IImageRepository {

        void Add(Image image);
        List<Image> GetAll();
        Image GetById(Guid id);
        void Remove(Image image);
        void SaveChanges();

    }

}