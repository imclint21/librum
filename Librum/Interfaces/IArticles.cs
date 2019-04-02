using System.Collections.Generic;
using System.Threading.Tasks;
using Librum.Models;

namespace Librum.Interfaces
{
    public interface IArticles
    {
        Task NewArticleAsync(Article article);
        Task<List<Article>> GetAllAsync();
        Task<Article> GetArticleBySlugAsync(string slug);
        
    }
}