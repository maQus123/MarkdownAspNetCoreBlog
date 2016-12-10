namespace MarkdownAspNetCoreBlog.Repositories.Tags {

    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TagRepository : ITagRepository {

        private readonly DataContext dataContext;

        public TagRepository(DataContext dataContext) {
            this.dataContext = dataContext;
        }

        public void Add(Tag tag) {
            this.dataContext.Tags.Add(tag);
            this.SaveChanges();
            return;
        }

        public List<Tag> GetAll() {
            var tags = this.dataContext.Tags.OrderBy(t => t.Title).ToList();
            return tags;
        }

        public Tag GetById(Guid id) {
            var tag = this.dataContext.Tags.Single(t => t.Id == id);
            return tag;
        }

        public Tag GetBySlug(string slug) {
            var tag = this.dataContext.Tags.SingleOrDefault(t => t.Slug() == slug);
            return tag;
        }

        public void Remove(Tag tag) {
            this.dataContext.Tags.Remove(tag);
            this.SaveChanges();
            return;
        }

        public void SaveChanges() {
            this.dataContext.SaveChanges();
            return;
        }

        public void Update(Tag tag) {
            this.dataContext.Tags.Update(tag);
            this.SaveChanges();
            return;
        }

    }

}