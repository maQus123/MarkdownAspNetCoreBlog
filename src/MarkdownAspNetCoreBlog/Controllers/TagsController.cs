namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.Linq;

    public class TagsController : Controller {

        private DataContext dataContext;

        public TagsController(DataContext dataContext) {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title")] Tag tag) {
            if (ModelState.IsValid) {
                this.dataContext.Tags.Add(tag);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return View(tag);
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id) {
            var tag = this.dataContext.Tags.Single(t => t.Id == id);
            if (null != tag) {
                return View(tag);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id) {
            var tag = this.dataContext.Tags.Single(t => t.Id == id);
            if (null != tag) {
                this.dataContext.Tags.Remove(tag);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult List() {
            var tags = this.dataContext.Tags
                .OrderBy(t => t.Title)
                .ToList();
            return View(tags);
        }

    }

}