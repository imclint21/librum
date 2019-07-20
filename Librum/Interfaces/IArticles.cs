using System.Collections.Generic;
using System.Threading.Tasks;
using Librum.Models;

namespace Librum.Interfaces
{
    public interface IArticles
    {
        Task NewArticleAsync(Article article);
        Task EditArticleAsync(string slugArticle, Article article);
        Task DeleteArticleAsync(string slugArticle);
        Task PublishArticleAsync(string slugArticle);
        Task<List<Article>> GetAllAsync();
        Task<List<Article>> GetSearchResultAsync(string terms);
        Task<Article> GetArticleBySlugAsync(string slug);
        
    }
}