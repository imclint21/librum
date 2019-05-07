using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Librum.Models;
using Librum.Settings;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Librum.Interfaces
{
    public class Articles : IArticles
    {
        private DatabaseContext _databaseContext;

        public Articles(DatabaseContext databasecontext)
        {
            _databaseContext = databasecontext;
        }

        public async Task NewArticleAsync(Article article)
        {
            _databaseContext.Articles.Add(article);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task EditArticleAsync(string slugArticle, Article article)
        {
            if(_databaseContext.Articles.Any(x => x.Slug.Equals(slugArticle)))
            {
                var previousArticle = _databaseContext.Articles.First(x => x.Slug == slugArticle);
                previousArticle.Slug = article.Slug;
                previousArticle.WritedDatetime = DateTime.Now;
                previousArticle.Title = article.Title;
                previousArticle.Content = article.Content;
                previousArticle.Description = Markdown.ToPlainText(article.Content);
                previousArticle.AuthorUsername = article.AuthorUsername;
                previousArticle.Keywords = article.Keywords;
                previousArticle.IsDraft = article.IsDraft;
                previousArticle.Unlisted = article.Unlisted;
                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task DeleteArticleAsync(string slugArticle)
        {
            var article = _databaseContext.Articles.First(x => x.Slug == slugArticle);
            _databaseContext.Articles.Remove(article);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await _databaseContext.Articles.ToListAsync();
        }

        public async Task<Article> GetArticleBySlugAsync(string slugArticle)
        {
            var article = await _databaseContext.Articles.FirstOrDefaultAsync(x => x.Slug == slugArticle);
            return article;
        }
    }
}