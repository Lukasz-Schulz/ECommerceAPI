using System.Collections.Generic;

namespace ECommerceApp.Models
{
    /// <summary>
    /// Repository schema for article.
    /// </summary>
    public class Article
    {
        public string ArticleNo { get; set; }
        public string ArticleName { get; set; }
        public string Description { get; set; }
        public decimal PriceInSEK { get; set; }

        public Article(string ArticleNo, string ArticleName, string Description, string Category, decimal PriceInSEK)
        {
            this.ArticleNo = ArticleNo;
            this.ArticleName = ArticleName;
            this.Description = Description;
            this.PriceInSEK = PriceInSEK;
        }
    }
}