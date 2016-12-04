namespace MarkdownAspNetCoreBlog.ViewModels.Posts {

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using System.Collections.Generic;


    public class UpdatePostViewModel {

        public UpdatePostViewModel() {
            this.AllTags = new List<SelectListItem>();
            this.SelectedTags = new List<string>();
        }

        public UpdatePostViewModel(Post post, List<Tag> tags) : this() {
            this.Post = post;
            foreach (var tag in tags) {
                this.AllTags.Add(new SelectListItem { Text = tag.Title, Value = tag.Id.ToString() });
            }
            foreach (var postTag in post.PostTags) {
                this.SelectedTags.Add(postTag.TagId.ToString());
            }
        }

        public Post Post { get; set; }

        public List<SelectListItem> AllTags { get; private set; }

        public List<string> SelectedTags { get; private set; }

    }

}