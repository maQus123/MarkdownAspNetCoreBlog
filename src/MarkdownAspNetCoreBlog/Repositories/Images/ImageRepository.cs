namespace MarkdownAspNetCoreBlog.Repositories.Images {

    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ImageRepository : IImageRepository {

        private readonly DataContext dataContext;

        public ImageRepository(DataContext dataContext) {
            this.dataContext = dataContext;
        }

        public void Add(Image image) {
            this.dataContext.Images.Add(image);
            this.SaveChanges();
            return;
        }

        public List<Image> GetAll() {
            var images = this.dataContext.Images.OrderBy(i => i.Title).ToList();
            return images;
        }

        public Image GetById(Guid id) {
            var image = this.dataContext.Images.Single(i => i.Id == id);
            return image;
        }

        public void Remove(Image image) {
            this.dataContext.Images.Remove(image);
            this.dataContext.SaveChanges();
            return;
        }

        public void SaveChanges() {
            this.dataContext.SaveChanges();
            return;
        }

    }

}