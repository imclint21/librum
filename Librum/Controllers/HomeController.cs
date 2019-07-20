using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Librum.Models;
using Librum.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Librum.Controllers
{
    public class HomeController : Controller
    {
        private readonly Articles _articles;

        public HomeController(Articles articles)
        {
            _articles = articles;

        }
        
        public async Task<IActionResult> Index()
        {
            var articles = await _articles.GetAllAsync();
            if(!User.Identity.IsAuthenticated)
            {
                articles = articles.Where(x => !x.IsDraft).ToList();
            }
            return View(articles.OrderByDescending(x => x.WritedDatetime).ToList());
        }

        [Authorize]
        [Route("about")]
        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        [Route("settings")]
        public IActionResult Settings()
        {
            return View();
        }

        [Route("search")]
        public async Task<IActionResult> Search(string terms)
        {
            if(string.IsNullOrWhiteSpace(terms))
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Terms = terms;
            var articles = await _articles.GetSearchResultAsync(terms);
            if (!User.Identity.IsAuthenticated)
            {
                articles = articles.Where(x => !x.IsDraft).ToList();
            }
            return View(articles.OrderByDescending(x => x.WritedDatetime).ToList());
        }

        [Route("saved-articles")]
        public async Task<IActionResult> SavedArticles()
        {
            var bookmarks = new List<string>();
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Bookmarks")))
            {
                bookmarks = JsonConvert.DeserializeObject<string[]>(HttpContext.Session.GetString("Bookmarks")).ToList();
            }
            var articles = await _articles.GetSavedArticlesAsync(bookmarks);
            return View(articles);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
