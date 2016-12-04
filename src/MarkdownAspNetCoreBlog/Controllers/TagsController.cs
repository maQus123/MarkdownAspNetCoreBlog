namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Repositories;
    using System;
    using ViewModels.Tags;

    public class TagsController : Controller {

        private readonly ITagRepository tagRepository;

        public TagsController(ITagRepository tagRepository) {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NewTag")] CreateTagViewModel viewModel) {
            if (ModelState.IsValid) {
                var tag = new Tag(viewModel.NewTag);
                this.tagRepository.Add(tag);
                return RedirectToAction("List");
            } else {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id) {
            var tag = this.tagRepository.GetById(id);
            if (null != tag) {
                var viewModel = new DeleteTagViewModel(tag);
                return View(viewModel);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id) {
            var tag = this.tagRepository.GetById(id);
            if (null != tag) {
                this.tagRepository.Remove(tag);
                return RedirectToAction("List");
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult List() {
            var tags = this.tagRepository.GetAll();
            var viewModel = new ListTagsViewModel(tags);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(Guid id) {
            var tag = this.tagRepository.GetById(id);
            if (null != tag) {
                var viewModel = new UpdateTagViewModel(tag);
                return View(viewModel);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid id, [Bind("Tag")] UpdateTagViewModel viewModel) {
            var existingTag = this.tagRepository.GetById(id);
            if (null != existingTag) {
                existingTag.UpdateFrom(viewModel.Tag);
                this.tagRepository.Update(existingTag);
                return RedirectToAction("List");
            } else {
                return View(viewModel);
            }
        }

    }

}