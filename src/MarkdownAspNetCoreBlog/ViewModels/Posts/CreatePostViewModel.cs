namespace MarkdownAspNetCoreBlog.ViewModels.Posts {

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using System.Collections.Generic;

    public class CreatePostViewModel {

        public CreatePostViewModel() {
            this.NewPost = new Post();
            this.AllTags = new List<SelectListItem>();
            this.SelectedTags = new List<string>();
        }

        public CreatePostViewModel(List<Tag> tags) : this() {
            foreach (var tag in tags) {
                this.AllTags.Add(new SelectListItem { Text = tag.Title, Value = tag.Id.ToString() });
            }
        }

        public Post NewPost { get; set; }

        public List<SelectListItem> AllTags { get; private set; }

        public List<string> SelectedTags { get; set; }

    }

}