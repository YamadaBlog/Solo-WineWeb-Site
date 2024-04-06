using Sukuna.Common.Models;
using Sukuna.Common.Resources;

namespace Sukuna.Business.Interfaces;

// Besoin de boook resources from common bcs it takes the the parameters reuse in serive 

public interface IArticleService
{
    Article GetArticleTrimToUpper(ArticleResource articleCreate);
    bool CreateArticle(int clientOrderID, int supplierOrderID, Article article);
    ICollection<Article> GetArticles();
    bool Save();
}