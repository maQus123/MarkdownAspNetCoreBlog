namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.Linq;

    public class PostsController : Controller {

        private DataContext dataContext;

        public PostsController(DataContext dataContext) {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Content,IsPublished")] Post post) {
            if (ModelState.IsValid) {
                post.CreatedAt = new DateTimeOffset(DateTime.UtcNow);
                post.Id = Guid.NewGuid();
                this.dataContext.Posts.Add(post);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return View(post);
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id) {
            var post = this.dataContext.Posts.Single(p => p.Id == id);
            if (null != post) {
                return View(post);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id) {
            var post = this.dataContext.Posts.Single(p => p.Id == id);
            if (null != post) {
                this.dataContext.Posts.Remove(post);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Details(string year, string month, string slug) {
            var post = this.dataContext.Posts.Single(p => p.Slug() == slug && p.CreatedAt.ToString("yyyyMM") == string.Concat(year, month));
            if (null != post && post.IsPublished) {
                return View(post);
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Index() {
            var posts = this.dataContext.Posts
                .Where(p => p.IsPublished)
                .OrderBy(p => p.CreatedAt)
                .ToList();
            return View(posts);
        }

        [HttpGet]
        public IActionResult List() {
            var posts = this.dataContext.Posts
                .OrderBy(p => p.CreatedAt)
                .ToList();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Update(Guid id) {
            var post = this.dataContext.Posts.Single(p => p.Id == id);
            if (null != post) {
                return View(post);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid id, [Bind("Id,Title,Content,IsPublished")] Post post) {
            var existingPost = this.dataContext.Posts.Single(p => p.Id == id);
            if (null != existingPost && id == post.Id) {
                if (ModelState.IsValid) {
                    existingPost.Title = post.Title;
                    existingPost.Content = post.Content;
                    existingPost.IsPublished = post.IsPublished;
                    this.dataContext.SaveChanges();
                    return RedirectToAction("List");
                } else {
                    return View(post);
                }
            } else {
                return NotFound();
            }
        }

    }

}