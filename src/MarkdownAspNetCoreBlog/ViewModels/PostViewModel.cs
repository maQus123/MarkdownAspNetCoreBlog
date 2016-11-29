namespace MarkdownAspNetCoreBlog.ViewModels {

    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PostViewModel {

        public PostViewModel() {
            this.AllTags = new List<SelectListItem>();
            this.SelectedTags = new List<string>();
        }

        public PostViewModel(string title, string content, bool isPublished, List<SelectListItem> allTags, List<string> selectedTags) {
            this.Title = title;
            this.Content = content;
            this.IsPublished = isPublished;
            this.AllTags = allTags;
            this.SelectedTags = selectedTags;
        }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsPublished { get; set; }

        public List<SelectListItem> AllTags { get; private set; }

        public List<string> SelectedTags { get; private set; }

    }

}