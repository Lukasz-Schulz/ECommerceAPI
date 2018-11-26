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
    [Route("Delete")]
    public class DeleteController : Controller
    {
        /// <summary>
        /// Deletes article from database by passed article number.
        /// </summary>
        [HttpPost]
        public IActionResult Delete([FromForm]string ArticleNo, [FromForm]string Category)
        {
            JSONData jSONData = new JSONData();

            if(jSONData.Delete(ArticleNo, Category))
            {
                return View("DeletedRecord", ArticleNo);
            }
            else
            {
                return View("Error", $"Article number {ArticleNo} not found.");
            }
        }
    }
}
