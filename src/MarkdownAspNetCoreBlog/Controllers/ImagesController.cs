namespace MarkdownAspNetCoreBlog.Controllers {

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Repositories.Images;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using ViewModels.Images;

    public class ImagesController : Controller {

        private readonly IImageRepository imageRepository;
        private IHostingEnvironment environment;
        private const string IMAGE_FOLDER = "img";

        public ImagesController(IImageRepository imageRepository, IHostingEnvironment environment) {
            this.imageRepository = imageRepository;
            this.environment = environment;
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateImageViewModel viewModel, IFormFile file) {
            if (file.Length > 0) {
                var folder = Path.Combine(this.environment.WebRootPath, IMAGE_FOLDER);
                using (var fileStream = new FileStream(Path.Combine(folder, (string)file.FileName), FileMode.Create)) {
                    await file.CopyToAsync(fileStream);
                }
                var image = new Image() { FolderName = IMAGE_FOLDER, Name = file.FileName };
                this.imageRepository.Add(image);
                return RedirectToAction("List");
            } else {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(Guid id) {
            var image = this.imageRepository.GetById(id);
            if (null != image) {
                var viewModel = new DeleteImageViewModel(image);
                return View(viewModel);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id) {
            var image = this.imageRepository.GetById(id);
            if (null != image) {
                var path = Path.Combine(this.environment.WebRootPath, image.FolderName, image.Name);
                System.IO.File.Delete(path);
                this.imageRepository.Remove(image);
                return RedirectToAction("List");
            } else {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult List() {
            var images = this.imageRepository.GetAll();
            var viewModel = new ListImagesViewModel(images);
            return View(viewModel);
        }

    }

}