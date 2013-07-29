using System.Linq;
using Nancy;
using Epic.Database;
using Epic.Models;

namespace Epic.Modules
{
    public class PostModule : NancyModule
    {       
        public PostModule (IEpicDatabase db)
        {
            // Show post by slug
            Get ["/posts/{slug}"] = parameters =>
            {
                string slug = parameters.slug;
                Post post = db.Posts.FirstOrDefault (i => i.Slug == slug);
                ViewBag.PostTitle = post.Title;

                return View ["View", post];
            };
        }
    }
}