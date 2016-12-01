namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.Linq;
    using ViewModels.Tags;

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
        public IActionResult Create([Bind("Tag")] TagViewModel viewModel) {
            if (ModelState.IsValid) {
                var tag = new Tag(viewModel.Tag);
                this.dataContext.Tags.Add(tag);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id) {
            var tag = this.dataContext.Tags.Single(t => t.Id == id);
            if (null != tag) {
                var viewModel = new TagViewModel(tag);
                return View(viewModel);
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
            var tags = this.dataContext.Tags.OrderBy(t => t.Title).ToList();
            var viewModel = new TagsViewModel(tags);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(Guid id) {
            var tag = this.dataContext.Tags.Single(t => t.Id == id);
            if (null != tag) {
                var viewModel = new TagViewModel(tag);
                return View(viewModel);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid id, [Bind("Tag")] TagViewModel viewModel) {
            var existingTag = this.dataContext.Tags.Single(t => t.Id == id);
            if (null != existingTag) {
                existingTag.UpdateFrom(viewModel.Tag);
                this.dataContext.Tags.Update(existingTag);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return View(viewModel);
            }
        }

    }

}