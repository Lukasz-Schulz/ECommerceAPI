
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ECommerceApp.Models
{
    /// <summary>
    /// Class for creating dataview models from repository models of articles and providing them.
    /// </summary>
    public class ArticleViewModelCollection
    {
        public List<SelectListItem> CategoryNames { get; private set; } = new List<SelectListItem>();
        public List<ArticleViewModel> Articles { get; set; } = new List<ArticleViewModel>();

        public ArticleViewModelCollection()
        {
            var categories = new CategoryCollection();
            //List<string> categoryList = new List<string>();
            foreach (var item in categories.AvailibleCategories.Keys)
            {
                CategoryNames.Add(new SelectListItem { Text=item, Value=item});
            }
        }

        public void Add(ArticleViewModel article)
        {
            Articles.Add(article);
        }

        public void FillWithCategory(string categoryName, Category category)
        {
            foreach(var article in category.Articles)
            {
                Articles.Add(new ArticleViewModel(article, categoryName));
            }
        }

        public void FillWithAllData()
        {
            CategoryCollection categoryCollection = new CategoryCollection();
            JSONData jSONData = new JSONData();
            categoryCollection = jSONData.GetAll(categoryCollection);

            foreach (var category in categoryCollection.AvailibleCategories)
            {
                FillWithCategory(category.Key, category.Value);
            }
        }
    }
}