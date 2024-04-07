using Sukuna.Common.Models;
using Sukuna.Common.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukuna.Business.Interfaces;

public interface IArticleService
{
    bool CreateArticle(Article article);
    Article GetArticleById(int articleId);
    ICollection<Article> GetArticles();
    ICollection<OrderLine> GetOrderLinesByArticle(int articleId);
    ICollection<Article> GetArticlesOfASupplier(int supplierId);
    bool UpdateArticle(Article article);
    bool DeleteArticle(Article article);
    Article ArticleExists(ArticleResource articleCreate);
    bool ArticleExistsById(int articleId);

    bool Save();
}
