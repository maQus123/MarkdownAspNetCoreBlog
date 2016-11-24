namespace MarkdownAspNetCoreBlog {

    using Microsoft.EntityFrameworkCore;
    using Models;

    public class DataContext : DbContext {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) {
            //nothing to do
        }

        public DbSet<Post> Posts { get; set; }

    }

}