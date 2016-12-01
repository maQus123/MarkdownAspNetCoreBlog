namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Posts;

    public class PostsController : Controller {

        private DataContext dataContext;

        public PostsController(DataContext dataContext) {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult Create() {
            var tags = this.dataContext.Tags.OrderBy(t => t.Title).ToList();
            var post = new Post();
            var allTags = new List<SelectListItem>();
            foreach (var tag in tags) {
                allTags.Add(new SelectListItem {
                    Text = tag.Title,
                    Value = tag.Id.ToString()
                });
            }
            var viewModel = new PostViewModel(post, allTags, new List<string>());
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Post,SelectedTags")] PostViewModel viewModel) {
            if (ModelState.IsValid) {
                var post = new Post(viewModel.Post);
                this.dataContext.Posts.Add(post);
                Guid guid;
                foreach (var selectedTag in viewModel.SelectedTags) {
                    if (Guid.TryParse(selectedTag, out guid)) {
                        var tag = this.dataContext.Tags.Single(t => t.Id == guid);
                        var postTag = new PostTag(post, tag);
                        this.dataContext.PostTags.Add(postTag);
                    }
                }
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id) {
            var post = this.dataContext.Posts.Single(p => p.Id == id);
            if (null != post) {
                var viewModel = new PostViewModel(post, null, null);
                return View(viewModel);
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
                var viewModel = new PostViewModel(post, null, null);
                return View(viewModel);
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Index() {
            var posts = this.dataContext.Posts
                .Where(p => p.IsPublished)
                .OrderBy(p => p.CreatedAt)
                .Include(p => p.PostTags)
                .ThenInclude(p => p.Tag)
                .ToList();
            var viewModel = new PostsViewModel(posts);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult List() {
            var posts = this.dataContext.Posts
                .OrderBy(p => p.CreatedAt)
                .Include(p => p.PostTags)
                .ThenInclude(p => p.Tag)
                .ToList();
            var viewModel = new PostsViewModel(posts);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(Guid id) {
            var post = this.dataContext.Posts.Include(p => p.PostTags).Single(p => p.Id == id);
            if (null != post) {
                var tags = this.dataContext.Tags.OrderBy(t => t.Title).ToList();
                var selectList = new List<SelectListItem>();
                foreach (var tag in tags) {
                    selectList.Add(new SelectListItem {
                        Text = tag.Title,
                        Value = tag.Id.ToString()
                    });
                }
                var selectedTags = new List<string>();
                foreach (var postTag in post.PostTags) {
                    selectedTags.Add(postTag.TagId.ToString());
                }
                var viewModel = new PostViewModel(post, selectList, selectedTags);
                return View(viewModel);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid id, [Bind("Post,SelectedTags")] PostViewModel viewModel) {
            if (ModelState.IsValid) {
                var post = this.dataContext.Posts.Include(p => p.PostTags).Single(p => p.Id == id);
                if (null != post && id == post.Id) {
                    post.UpdateFrom(viewModel.Post);
                    this.dataContext.Posts.Update(post);
                    var postTags = this.dataContext.PostTags.Where(p => p.PostId == post.Id);
                    this.dataContext.PostTags.RemoveRange(postTags);
                    this.dataContext.SaveChanges();
                    foreach (var selectedTag in viewModel.SelectedTags) {
                        var tag = this.dataContext.Tags.Single(t => t.Id.ToString() == selectedTag);
                        var postTag = new PostTag(post, tag);
                        this.dataContext.PostTags.Add(postTag);
                    }
                    this.dataContext.SaveChanges();
                    return RedirectToAction("List");
                } else {
                    return NotFound();
                }
            } else {
                return View(viewModel);
            }

        }

    }

}