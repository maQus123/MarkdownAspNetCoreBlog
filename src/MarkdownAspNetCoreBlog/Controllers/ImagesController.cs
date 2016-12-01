namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;
    using System.IO;
    using System.Linq;
    using ViewModels.Images;

    public class ImagesController : Controller {

        private DataContext dataContext;
        private IHostingEnvironment environment;
        private const string IMAGE_FOLDER = "img";

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
        public IActionResult Create([Bind("File")] IFormFile file) {
            if (file.Length > 0) {
                var folder = Path.Combine(this.environment.WebRootPath, IMAGE_FOLDER);
                using (var fileStream = new FileStream(Path.Combine(folder, file.FileName), FileMode.Create)) {
                    file.CopyTo(fileStream);
                }
                var image = new Image();
                image.FolderName = IMAGE_FOLDER;
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
                var viewModel = new ImageViewModel(image);
                return View(viewModel);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id) {
            var image = this.dataContext.Images.Single(i => i.Id == id);
            if (null != image) {
                var path = Path.Combine(this.environment.WebRootPath, image.FolderName, image.Name);
                System.IO.File.Delete(path);
                this.dataContext.Images.Remove(image);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult List() {
            var images = this.dataContext.Images.OrderBy(i => i.Name).ToList();
            var viewModel = new ImagesViewModel(images);
            return View(viewModel);
        }

    }

}