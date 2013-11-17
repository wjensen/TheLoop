using Nancy;

namespace TheLoop.Web
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                var model = new { title = "The Loop" };
                return View["home", model];
            };
        }
    }
}