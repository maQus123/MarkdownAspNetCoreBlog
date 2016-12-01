namespace MarkdownAspNetCoreBlog {

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup {

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase());
            services.Configure<RouteOptions>(options => {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });
            return;
        }

        public void Configure(IApplicationBuilder app) {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc(m => {
                m.MapRoute(
                    name: "index",
                    template: "",
                    defaults: new { controller = "Posts", action = "Index" });
                m.MapRoute(
                    name: "details",
                    template: "{year}/{month}/{slug}",
                    constraints: new { year = @"\d{4}", month = @"\d{2}", slug = @"^[a-z0-9-]+$" },
                    defaults: new { controller = "Posts", action = "Details" });
                m.MapRoute(
                    name: "create-post",
                    template: "posts/create",
                    defaults: new { controller = "Posts", action = "Create" });
                m.MapRoute(
                    name: "update-post",
                    template: "posts/edit/{id:guid}",
                    defaults: new { controller = "Posts", action = "Update" });
                m.MapRoute(
                    name: "list-posts",
                    template: "posts",
                    defaults: new { controller = "Posts", action = "List" });
                m.MapRoute(
                    name: "delete-post",
                    template: "posts/delete/{id:guid}",
                    defaults: new { controller = "Posts", action = "Delete" });
                m.MapRoute(
                    name: "delete-post-confirmed",
                    template: "posts/delete/{id:guid}",
                    defaults: new { controller = "Posts", action = "DeleteConfirmed" });
                m.MapRoute(
                    name: "create-image",
                    template: "images/create",
                    defaults: new { controller = "Images", action = "Create" });
                m.MapRoute(
                    name: "list-images",
                    template: "images",
                    defaults: new { controller = "Images", action = "List" });
                m.MapRoute(
                    name: "delete-image",
                    template: "images/delete/{id:guid}",
                    defaults: new { controller = "Images", action = "Delete" });
                m.MapRoute(
                    name: "delete-image-confirmed",
                    template: "images/delete/{id:guid}",
                    defaults: new { controller = "Images", action = "DeleteConfirmed" });
                m.MapRoute(
                    name: "create-tag",
                    template: "tags/create",
                    defaults: new { controller = "Tags", action = "Create" });
                m.MapRoute(
                    name: "list-tags",
                    template: "tags",
                    defaults: new { controller = "Tags", action = "List" });
                m.MapRoute(
                    name: "delete-tag",
                    template: "tags/delete/{id:guid}",
                    defaults: new { controller = "Tags", action = "Delete" });
                m.MapRoute(
                    name: "delete-tag-confirmed",
                    template: "tags/delete/{id:guid}",
                    defaults: new { controller = "Tags", action = "DeleteConfirmed" });
                m.MapRoute(
                    name: "update-tag",
                    template: "tags/edit/{id:guid}",
                    defaults: new { controller = "Tags", action = "Update" });
            });
            return;
        }

    }

}