namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
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
            var viewModel = new CreatePostViewModel(tags);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NewPost,SelectedTags")] CreatePostViewModel viewModel) {
            if (ModelState.IsValid) {
                var post = new Post(viewModel.NewPost);
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
                var viewModel = new DeletePostViewModel(post);
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
            var post = this.dataContext.Posts
                .Include(p => p.Comments)
                .Include(p => p.PostTags)
                .ThenInclude(p => p.Tag)
                .Single(p => p.Slug() == slug && p.CreatedAt.ToString("yyyyMM") == string.Concat(year, month));
            if (null != post && post.IsPublished) {
                var viewModel = new DetailsPostViewModel(post);
                return View(viewModel);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(string year, string month, string slug, [Bind("NewComment")] DetailsPostViewModel viewModel) {
            var post = this.dataContext.Posts
            .Include(p => p.Comments)
            .Single(p => p.Slug() == slug && p.CreatedAt.ToString("yyyyMM") == string.Concat(year, month));
            var comment = viewModel.NewComment;
            if (null != post && post.IsPublished && null != comment) {
                if (ModelState.IsValid) {
                    post.Comments.Add(comment);
                    this.dataContext.SaveChanges();
                    return RedirectToAction("Details", new { year = year, month = month, slug = slug });
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Index() {
            var posts = this.dataContext.Posts
                .Where(p => p.IsPublished)
                .OrderBy(p => p.CreatedAt)
                .Include(p => p.PostTags)
                .ThenInclude(p => p.Tag)
                .ToList();
            var viewModel = new ListPostsViewModel(posts);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult List() {
            var posts = this.dataContext.Posts
                .OrderBy(p => p.CreatedAt)
                .Include(p => p.PostTags)
                .ThenInclude(p => p.Tag)
                .ToList();
            var viewModel = new ListPostsViewModel(posts);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(Guid id) {
            var post = this.dataContext.Posts.Include(p => p.PostTags).Single(p => p.Id == id);
            if (null != post) {
                var tags = this.dataContext.Tags.OrderBy(t => t.Title).ToList();
                var viewModel = new UpdatePostViewModel(post, tags);
                return View(viewModel);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Guid id, [Bind("Post,SelectedTags")] UpdatePostViewModel viewModel) {
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