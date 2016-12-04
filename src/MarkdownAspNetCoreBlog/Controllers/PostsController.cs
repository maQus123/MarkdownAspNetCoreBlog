namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Repositories.Posts;
    using Repositories.Tags;
    using System;
    using ViewModels.Posts;

    public class PostsController : Controller {

        private readonly IPostRepository postRepository;
        private readonly ITagRepository tagRepository;

        public PostsController(IPostRepository postRepository, ITagRepository tagRepository) {
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Create() {
            var tags = this.tagRepository.GetAll();
            var viewModel = new CreatePostViewModel(tags);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("NewPost,SelectedTags")] CreatePostViewModel viewModel) {
            if (ModelState.IsValid) {
                var post = new Post(viewModel.NewPost);
                this.postRepository.Add(post);
                Guid id;
                foreach (var selectedTag in viewModel.SelectedTags) {
                    if (Guid.TryParse(selectedTag, out id)) {
                        var tag = this.tagRepository.GetById(id);
                        var postTag = new PostTag(post, tag);
                        this.postRepository.AddPostTagRelationship(postTag);
                    }
                }
                this.postRepository.SaveChanges();
                return RedirectToAction("List");
            } else {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id) {
            var post = this.postRepository.GetById(id);
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
            var post = this.postRepository.GetById(id);
            if (null != post) {
                this.postRepository.Remove(post);
                this.postRepository.SaveChanges();
                return RedirectToAction("List");
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Details(string year, string month, string slug) {
            var post = this.postRepository.GetByUrl(year, month, slug);
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
            var post = this.postRepository.GetByUrl(year, month, slug);
            var newComment = viewModel.NewComment;
            if (null != post && post.IsPublished && null != newComment) {
                if (ModelState.IsValid) {
                    post.Comments.Add(newComment);
                    this.postRepository.SaveChanges();
                    return RedirectToAction("Details", new { year = year, month = month, slug = slug });
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Index() {
            var posts = this.postRepository.GetAllPublished();
            var viewModel = new ListPostsViewModel(posts);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult List() {
            var posts = this.postRepository.GetAll();
            var viewModel = new ListPostsViewModel(posts);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Update(Guid id) {
            var post = this.postRepository.GetById(id);
            if (null != post) {
                var tags = this.tagRepository.GetAll();
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
                var post = this.postRepository.GetById(id);
                if (null != post && id == post.Id) {
                    post.UpdateFrom(viewModel.Post);
                    this.postRepository.Update(post);
                    var postTags = this.postRepository.GetPostTagRelationshipsByPostId(id);
                    this.postRepository.Remove(postTags);
                    this.postRepository.SaveChanges();
                    foreach (var selectedTag in viewModel.SelectedTags) {
                        var tag = this.tagRepository.GetById(Guid.Parse(selectedTag));
                        var postTag = new PostTag(post, tag);
                        this.postRepository.AddPostTagRelationship(postTag);
                    }
                    this.postRepository.SaveChanges();
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