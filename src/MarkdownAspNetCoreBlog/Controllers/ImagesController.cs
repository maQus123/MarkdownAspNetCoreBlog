namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
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
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("file")] IFormFile file) {
            if (file.Length > 0) {
                var folder = Path.Combine(this.environment.WebRootPath, "img");
                using (var fileStream = new FileStream(Path.Combine(folder, file.FileName), FileMode.Create)) {
                    file.CopyTo(fileStream);
                }
                var image = new Image();
                image.Id = Guid.NewGuid();
                image.UploadedAt = new DateTimeOffset(DateTime.UtcNow);
                image.Name = file.FileName;
                this.dataContext.Images.Add(image);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id) {
            var image = this.dataContext.Images.Single(i => i.Id == id);
            if (null != image) {
                return View(image);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id) {
            var image = this.dataContext.Images.Single(i => i.Id == id);
            if (null != image) {
                this.dataContext.Images.Remove(image);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult List() {
            var images = this.dataContext.Images
                .OrderBy(i => i.Name)
                .ToList();
            return View(images);
        }

    }

}