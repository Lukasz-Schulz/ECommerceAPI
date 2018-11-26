namespace ECommerceApp.Models
{
    /// <summary>
    /// Viewmodel for article. Counts the netto price of product.
    /// </summary>
    public class ArticleViewModel
    {
        public string ArticleNo { get; set; }
        public string ArticleName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal PriceInSEK { get; set; }
        public decimal PriceExclVAT { get { return decimal.Round(PriceInSEK / (1+Constants.VAT), 2, System.MidpointRounding.AwayFromZero); } }

        public ArticleViewModel(Article article, string category)
        {
            this.ArticleNo = article.ArticleNo;
            this.ArticleName = article.ArticleName;
            this.Description = article.Description;
            this.Category = Utilities.UppercaseFirst(category);
            this.PriceInSEK = article.PriceInSEK;
        }
    }
}