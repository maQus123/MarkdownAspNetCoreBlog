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
        public IActionResult List() {
            var images = this.dataContext.Images
                .OrderBy(i => i.FileName)
                .ToList();
            return View(images);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("displayName,file")] string displayName, IFormFile file) {
            var folder = Path.Combine(this.environment.WebRootPath, "img");
            if (file.Length > 0 && !string.IsNullOrWhiteSpace(displayName)) {
                using (var fileStream = new FileStream(Path.Combine(folder, file.FileName), FileMode.Create)) {
                    file.CopyTo(fileStream);
                }
                var image = new Image();
                image.Id = Guid.NewGuid();
                image.FileName = file.FileName;
                image.DisplayName = displayName;
                this.dataContext.Images.Add(image);
                this.dataContext.SaveChanges();
                return RedirectToAction("List");
            } else {
                return View();
            }
        }
        
    }

}