using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Librum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace Librum.Interfaces
{
    public class Articles
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DatabaseContext _databaseContext;

        public Articles(IHttpContextAccessor httpContextAccessor, DatabaseContext databasecontext)
        {
            _httpContextAccessor = httpContextAccessor;
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
                _databaseContext.Articles.Update(article);
                await _databaseContext.SaveChangesAsync();
            }
        }

        public async Task DeleteArticleAsync(string slugArticle)
        {
            var article = _databaseContext.Articles.First(x => x.Slug == slugArticle);
            _databaseContext.Articles.Remove(article);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task PublishArticleAsync(string slugArticle)
        {
            var article = _databaseContext.Articles.First(x => x.Slug == slugArticle);
            article.IsDraft = false;
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<List<Article>> GetAllAsync() => await _databaseContext.Articles.ToListAsync();

        public async Task<Article> GetArticleBySlugAsync(string slugArticle)
        {
            return await _databaseContext.Articles.FirstOrDefaultAsync(x => x.Slug == slugArticle); ;
        }

        public async Task<List<Article>> GetSearchResultAsync(string terms) => await _databaseContext.Articles.Where(x => x.Title.Contains(terms) || x.Content.Contains(terms)).ToListAsync();

        public bool IsInBookmarks(string id)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Bookmarks") != null)
            {
                return JsonConvert.DeserializeObject<string[]>(_httpContextAccessor.HttpContext.Session.GetString("Bookmarks")).Contains(id);
            }
            return false;
        }

        public bool IsLiked(string id)
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Liked") != null)
            {
                return JsonConvert.DeserializeObject<string[]>(_httpContextAccessor.HttpContext.Session.GetString("Liked")).Contains(id);
            }
            return false;
        }
    }
}