namespace MarkdownAspNetCoreBlog.ViewModels.Posts {

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using System.Collections.Generic;

    public class PostViewModel {

        public PostViewModel() {
            this.Post = new Post();
            this.AllTags = new List<SelectListItem>();
            this.SelectedTags = new List<string>();
        }

        public PostViewModel(Post post, List<SelectListItem> allTags, List<string> selectedTags) {
            this.Post = post;
            this.AllTags = allTags;
            this.SelectedTags = selectedTags;
        }

        public Post Post { get; set; }

        public List<SelectListItem> AllTags { get; private set; }

        public List<string> SelectedTags { get; private set; }

    }

}