using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Librum.Models;
using Librum.Settings;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Article>> GetAllAsync()
        {
            return await _databaseContext.Articles.ToListAsync();
        }

        public async Task<Article> GetArticleBySlugAsync(string slug)
        {
            var article = await _databaseContext.Articles.FirstOrDefaultAsync(x => x.Slug.Equals(slug));
            return article;
        }
    }
}