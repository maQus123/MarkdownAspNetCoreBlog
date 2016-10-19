namespace MarkdownAspNetCoreBlog {

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

    public class Startup {

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc();
            services.AddSingleton<PostRepository>();
            services.Configure<RouteOptions>(options => {
                options.AppendTrailingSlash = true;
                options.LowercaseUrls = true;
            });
            return;
        }

        public void Configure(IApplicationBuilder app) {
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseMvc(m => {
                m.MapRoute(
                    name: "index",
                    template: "",
                    defaults: new { controller = "Posts", action = "Index" });
                m.MapRoute(
                    name: "details",
                    template: "{year}/{month}/{slug}/",
                    constraints: new { year = @"\d{4}", month = @"\d{2}", slug = @"^[a-z0-9-]+$" },
                    defaults: new { controller = "Posts", action = "Details" });
                m.MapRoute(
                    name: "create",
                    template: "posts/create",
                    defaults: new { controller = "Posts", action = "Create" });
                m.MapRoute(
                    name: "read",
                    template: "posts/details/{id:int}",
                    defaults: new { controller = "Posts", action = "Read" });
                m.MapRoute(
                    name: "update",
                    template: "posts/edit/{id:int}",
                    defaults: new { controller = "Posts", action = "Update" });
                m.MapRoute(
                    name: "list",
                    template: "posts",
                    defaults: new { controller = "Posts", action = "List" });
            });
            return;
        }

    }

}