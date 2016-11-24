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
        public IActionResult Index() {
            var posts = this.dataContext.Posts.OrderBy(p => p.CreatedAt).ToList();
            return View(posts);
        }

        [HttpGet]
        public IActionResult List() {
            var posts = this.dataContext.Posts.OrderBy(p => p.CreatedAt).ToList();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Details(string year, string month, string slug) {
            if (!string.IsNullOrWhiteSpace(year) && !string.IsNullOrWhiteSpace(month) && !string.IsNullOrWhiteSpace(slug)) {
                var date = string.Concat(year, month);
                var post = this.dataContext.Posts.Single(p => p.Slug() == slug && p.CreatedAt.ToString("yyyyMM") == date);
                if (null != post) {
                    return View(post);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Content")] Post post) {
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
        public IActionResult Update(Guid id) {
            var post = this.dataContext.Posts.Single(p => p.Id == id);
            if (null != post) {
                return View(post);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid id, [Bind("Id, Title,Content")] Post post) {
            if (id != post.Id) {
                return NotFound();
            } else {
                if (ModelState.IsValid) {
                    var existingPost = this.dataContext.Posts.Single(p => p.Id == id);
                    if (null != existingPost) {
                        existingPost.Title = post.Title;
                        existingPost.Content = post.Content;
                        this.dataContext.SaveChanges();
                        return RedirectToAction("List");
                    } else {
                        return NotFound();
                    }
                } else {
                    return View(post);
                }
            }
        }

    }

}