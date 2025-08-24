using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Premosh10.ViewModels;

namespace Premosh10.Controllers
{
    public class XSSDemoController : Controller
    {
        // Vulnerable version (for demonstration only)
        public IActionResult Vulnerable(string userComment)
        {
            var model = new CommentViewModel { Comment = userComment };
            return View(model);
        }

        // Safe version
        public IActionResult Safe(string userComment)
        {
            var model = new CommentViewModel { Comment = userComment };
            return View(model);
        }
    }
}
