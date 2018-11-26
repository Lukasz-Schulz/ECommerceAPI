using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Models
{
    /// <summary>
    /// Separate entity for shared category property.
    /// </summary>
    public class Category
    {
        public List<Article> Articles { get; set; } = new List<Article>();
        public void Add(Article article)
        {
            Articles.Add(article);
        }
    }
}
