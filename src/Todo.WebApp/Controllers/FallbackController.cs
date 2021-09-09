using System;
using Microsoft.AspNetCore.Mvc;

namespace Todo.WebApp.Controllers
{
    public class FallbackController : Controller
    {
        public IActionResult HtmlNotFoundFallback(string slug)
        {
            return this.NotFoundView();
        }

        public IActionResult ApiNotFoundFallback(string slug)
        {
            return this.NotFoundObject();
        }
    }
}
