using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Markdig;
using Slugify;
using Librum.Interfaces;
using Librum.Models;

namespace Librum.Controllers
{
    [Authorize]
    [Route("article")]
    public class ArticleController : Controller
    {
        private readonly Articles _articles;

        public ArticleController(Articles articles)
        {
            _articles = articles;
        }
        
        [Route("new-post")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [Route("new-post")]
        public async Task<IActionResult> New(Article article)
        {
            if(ModelState.IsValid)
            {
                article.Slug = Slug(article.Title);
                article.Title = article.Title;
                article.WritedDatetime = DateTime.Now;
                article.Content = article.Content;
                article.Description = Truncate(Markdown.ToPlainText(article.Content), 300, "…", true);
                article.AuthorUsername = User.Identity.Name;
                article.Keywords = article.Keywords;
                article.IsDraft = article.IsDraft;
                article.Unlisted = article.Unlisted;
                article.LikeCount = 0;
                article.ReadCount = 0;
                await _articles.NewArticleAsync(article);
                return RedirectToAction("Article", new { slugArticle = article.Slug });
            }
            return View(article);
        }
        
        [AllowAnonymous]
        [Route("{slugArticle}")]
        public async Task<IActionResult> Article(string slugArticle)
        {
            var article = await _articles.GetArticleBySlugAsync(slugArticle);
            if(article == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(article);
        }

        [Route("{slugArticle}/edit")]
        public async Task<IActionResult> Edit(string slugArticle)
        {
            var article = await _articles.GetArticleBySlugAsync(slugArticle);
            return View(article);
        }

        [HttpPost]
        [Route("{slugArticle}/edit")]
        public async Task<IActionResult> Edit(string slugArticle, Article article)
        {
            if (ModelState.IsValid)
            {
                article.Slug = Slug(article.Title);
                article.Title = article.Title;
                article.WritedDatetime = DateTime.Now;
                article.Content = article.Content;
                article.Description = Truncate(Markdown.ToPlainText(article.Content), 300, "…", true);
                article.AuthorUsername = User.Identity.Name;
                article.Keywords = article.Keywords;
                article.IsDraft = article.IsDraft;
                article.Unlisted = article.Unlisted;
                await _articles.EditArticleAsync(slugArticle, article);
                return RedirectToAction("Article", new { slugArticle = article.Slug });
            }
            return View(article);
        }

        [Route("{slugArticle}/delete")]
        public async Task<IActionResult> Delete(string slugArticle)
        {
            await _articles.DeleteArticleAsync(slugArticle);
            return RedirectToAction("Index", "Home");
        }

        [Route("{slugArticle}/publish")]
        public async Task<IActionResult> Publish(string slugArticle)
        {
            await _articles.PublishArticleAsync(slugArticle);
            return RedirectToAction("Index", "Home");
        }

        private static string Truncate(string value, int length, string ellipsis, bool keepFullWordAtEnd)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.Length < length) return value;
            value = value.Substring(0, length);
            if (keepFullWordAtEnd)
            {
                value = value.Substring(0, value.LastIndexOf(' '));
            }
            return value + ellipsis;
        }

        private static string Slug(string value)
        {
            return new SlugHelper().GenerateSlug(RemoveSpecialCharacters(value).ToLower().Trim()).ToString();
        }

        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_. ]+", "", RegexOptions.Compiled);
        }
    }
}