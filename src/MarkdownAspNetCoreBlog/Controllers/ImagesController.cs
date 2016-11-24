namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.IO;
    using System.Linq;

    public class ImagesController : Controller {

        private DataContext dataContext;
        private IHostingEnvironment environment;

        public ImagesController(DataContext dataContext, IHostingEnvironment environment) {
            this.dataContext = dataContext;
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult List() {
            var images = this.dataContext.Images
                .OrderBy(i => i.Name)
                .ToList();
            return View(images);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile file) {
            var folder = Path.Combine(this.environment.WebRootPath, "img");
            if (file.Length > 0) {
                using (var fileStream = new FileStream(Path.Combine(folder, file.FileName), FileMode.Create)) {
                    file.CopyTo(fileStream);
                }
                var image = new Image();
                image.Name = file.FileName;
                this.dataContext.Images.Add(image);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return View();
            }            
        }

    }

}