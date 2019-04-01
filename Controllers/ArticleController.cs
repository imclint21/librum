using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Librum.Controllers
{
    [Authorize]
    [Route("article")]
    public class ArticleController : Controller
    {
        public ArticleController()
        {
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(string article)
        {
            return View();
        }

        [AllowAnonymous]
        [Route("{slugArticle}")]
        public IActionResult Article(string slugArticle)
        {
            return View();
        }
    }
}