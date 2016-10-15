namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;

    public class PostsController : Controller {

        public PostRepository Posts { get; set; }

        public PostsController(PostRepository posts) {
            this.Posts = posts;
        }

        [HttpGet]
        public IActionResult Index() {
            var posts = this.Posts.GetAll();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Details(string year, string month, string slug) {
            if (!string.IsNullOrWhiteSpace(year) && !string.IsNullOrWhiteSpace(month) && !string.IsNullOrWhiteSpace(slug)) {
                Post post = this.Posts.Find(year, month, slug);
                if (null != post) {
                    return View(post);
                } else {
                    return NotFound();
                }
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Content")] Post post) {
            if (!string.IsNullOrWhiteSpace(post.Title) && !string.IsNullOrWhiteSpace(post.Content)) {
                post.CreatedAt = new DateTimeOffset(DateTime.UtcNow);
                this.Posts.Add(post);
            }
            return RedirectToAction("Index");
        }

    }

}