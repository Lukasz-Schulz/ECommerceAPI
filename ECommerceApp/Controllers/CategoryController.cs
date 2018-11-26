using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerceApp.Models;

namespace ECommerceApp.Controllers
{
    [Produces("application/json")]
    [Route("{Category}")]
    public class CategoryController : Controller
    {
        /// <summary>
        /// Gets all articles of the same category.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {

            string category = (string)RouteData.Values["Category"];
            CategoryCollection categoryCollection = new CategoryCollection();
                var Collection = new ArticleViewModelCollection();
            if (categoryCollection.AvailibleCategories.ContainsKey(category))
            {
                JSONData jSONData = new JSONData();
                jSONData.GetAll(categoryCollection);
                var single = jSONData.GetCategory(category);
                Collection.FillWithCategory(category, single);
            }
                return View("Table", Collection);
        }

        /// <summary>
        /// Returns error view with error message.
        /// </summary>
        public IActionResult Get(string errorMsg)
        {
            return View("Error", "No such category.");
        }

        /// <summary>
        /// Updates or adds an article.
        /// </summary>
        [HttpPost]
        public IActionResult Update(
            [FromForm]string ArticleNo,
            [FromForm]string ArticleName,
            [FromForm]string Description,
            [FromForm]string Category,
            [FromForm]decimal PriceInSEK)

        {
            JSONData jSONData = new JSONData();
            string result = jSONData.Update(ArticleNo, ArticleName, Description, Category, PriceInSEK);
            
            if (result == "no such category") return Get(result);
            else return Get();
        }
    }
}
