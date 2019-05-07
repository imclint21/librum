using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Librum.Models;
using Microsoft.AspNetCore.Authorization;
using Librum.Interfaces;

namespace Librum.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticles _articles;

        public HomeController(IArticles articles)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
