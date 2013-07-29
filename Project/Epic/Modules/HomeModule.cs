using System.Linq;
using Nancy;
using Epic.Database;

namespace Epic.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule (IEpicDatabase db)
        {
            // Homepage; show list of all posts
            Get ["/"] = parameters =>
            {
                var posts = db.Posts.OrderByDescending (i => i.Created);

                return View ["Index", posts];
            };
        }
    }
}

