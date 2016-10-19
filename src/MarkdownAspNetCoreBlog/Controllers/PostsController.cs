namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class PostsController : Controller {

        public PostRepository PostRepository { get; set; }

        public PostsController(PostRepository postRepository) {
            this.PostRepository = postRepository;
        }

        [HttpGet]
        public IActionResult Index() {
            var posts = this.PostRepository.GetAll();
            return View(posts);
        }

        [HttpGet]
        public IActionResult List() {
            var posts = this.PostRepository.GetAll();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Details(string year, string month, string slug) {
            if (!string.IsNullOrWhiteSpace(year) && !string.IsNullOrWhiteSpace(month) && !string.IsNullOrWhiteSpace(slug)) {
                Post post = this.PostRepository.Find(year, month, slug);
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
                this.PostRepository.Add(post);
                return RedirectToAction("List");
            } else {
                return View(post);
            }                        
        }

        [HttpGet]
        public IActionResult Update(int id) {
            return this.GetPost(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, [Bind("Id, Title,Content")] Post post) {
            if (id != post.Id) {
                return NotFound();
            } else {
                if (ModelState.IsValid) {
                    this.PostRepository.Update(id, post);
                    return RedirectToAction("List");
                } else {
                    return View(post);
                }
            }
        }

        [HttpGet]
        public IActionResult Read(int id) {
            return this.GetPost(id);
        }

        private IActionResult GetPost(int id) {
            var existingPost = this.PostRepository.Find(id);
            if (null != existingPost) {
                return View(existingPost);
            } else {
                return NotFound();
            }
        }

    }

}