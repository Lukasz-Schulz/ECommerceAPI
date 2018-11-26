using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerceApp.Controllers
{
    [Route("")]
    public class MainController : Controller
    {
        /// <summary>
        /// Returns table with all records from database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var Collection = new ArticleViewModelCollection();
            Collection.FillWithAllData();

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
