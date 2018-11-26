using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceApp.Models
{
    public class JSONData
    {
        private readonly string JsonLocation = @"Resources/json.json";

        List<Category> QuerriedJSON = new List<Category>();

        string JSONString;
        private JObject _parsedJSON()
        {
            using (StreamReader reader = new StreamReader(JsonLocation))
            {
                JSONString = reader.ReadToEnd();
            }
        return JObject.Parse(JSONString);
        }

        /// <summary>
        /// Returns all articles from the database.
        /// </summary>
        public CategoryCollection GetAll(CategoryCollection categoryCollection)
        {
            CategoryCollection newCategoryCollection = new CategoryCollection();
            foreach(var category in categoryCollection.AvailibleCategories.Keys)
            {                
                newCategoryCollection.AvailibleCategories[category] = GetCategory(category);
            }
            return newCategoryCollection;
        }

        /// <summary>
        /// Returns all articles from specified category.
        /// </summary>
        public Category GetCategory(string category)
        {
            JObject parsedJson = _parsedJSON();
            if (parsedJson["AvailibleCategories"][category] != null)
            {
                var collection = parsedJson["AvailibleCategories"][category]["Articles"];

                CategoryCollection categoryCollection = new CategoryCollection();
                {
                    foreach (var element in collection)
                    {
                        categoryCollection.AvailibleCategories[category].Articles.Add(
                            new Article(
                                (string)element["ArticleNo"],
                                (string)element["ArticleName"],
                                (string)element["Description"],
                                (string)element["Category"],
                                (decimal)element["PriceInSEK"]
                            )
                        );
                    }
                }
                return categoryCollection.AvailibleCategories[category];
            }
            else
            {
                return new Category();
            }
        }

        /// <summary>
        /// Returns collection of articles with specified name.
        /// </summary>
        public Category GetName(string category, string name)
        {
            JObject parsedJson = _parsedJSON();
            var collection = parsedJson["AvailibleCategories"][category]["Articles"];

            CategoryCollection categoryCollection = new CategoryCollection();
            {
                foreach (var element in collection)
                {
                    if(element["ArticleName"].ToString().Equals(name.Replace('_', ' ').Replace('-', ' '), StringComparison.InvariantCultureIgnoreCase))
                    {
                        categoryCollection.AvailibleCategories[category].Articles.Add(
                            new Article(
                                (string)element["ArticleNo"],
                                (string)element["ArticleName"],
                                (string)element["Description"],
                                (string)element["Category"],
                                (decimal)element["PriceInSEK"]
                            )
                        );
                    }
                }
            }
            return categoryCollection.AvailibleCategories[category];
        }

        /// <summary>
        /// Adds new record to database and returns false or updates it and returns true.
        /// </summary>
        public string Update(string ArticleNo, string ArticleName, string Description, string Category, decimal PriceInSEK)
        {
            string lowercaseCategory = Category.ToLower();
            CategoryCollection categoryCollection = new CategoryCollection();
            JSONData jSONData = new JSONData();
            categoryCollection = jSONData.GetAll(categoryCollection);
            bool recordIdExists = false;
            try
            {
                foreach(var category in categoryCollection.AvailibleCategories)
                {
                    if (category.Value.Articles.Find(a=> a.ArticleNo.Equals(ArticleNo)) != null)
                    {
                        var art = category.Value.Articles.Find(a => a.ArticleNo.Equals(ArticleNo));
                        var indexer = category.Value.Articles.IndexOf(art);
                        category.Value.Articles[indexer] = new Article(ArticleNo, ArticleName.Replace('_', ' ').Replace('-', ' '), Description, lowercaseCategory, PriceInSEK);
                        recordIdExists = true;
                        break;
                    }
                }
                if (!recordIdExists)
                {
                    categoryCollection.AvailibleCategories[lowercaseCategory].Articles.Add(new Article(ArticleNo, ArticleName, Description, lowercaseCategory, PriceInSEK));
                }
                var updatedJson = JsonConvert.SerializeObject(categoryCollection, Formatting.Indented);
                File.WriteAllText(JsonLocation, updatedJson);
            }
            catch (KeyNotFoundException) { return "no such category"; }

            return "added";
        }

        /// <summary>
        /// Deletes record to database and returns true or returns false if no changes commited.
        /// </summary>
        public bool Delete(string ArticleNo, string Category)
        {
            CategoryCollection categoryCollection = new CategoryCollection();
            JSONData jSONData = new JSONData();
            categoryCollection = jSONData.GetAll(categoryCollection);
            bool recordIdExists = false;

            for (int i = 0; i < categoryCollection.AvailibleCategories[Category.ToLower()].Articles.Count; i++)
            {
                if (categoryCollection.AvailibleCategories[Category.ToLower()].Articles[i].ArticleNo.Equals(ArticleNo, StringComparison.InvariantCultureIgnoreCase)) 
                {
                    categoryCollection.AvailibleCategories[Category.ToLower()].Articles.RemoveAt(i);
                    recordIdExists = true;
                    break;
                }
            }

            var updatedJson = JsonConvert.SerializeObject(categoryCollection, Formatting.Indented);
            File.WriteAllText(JsonLocation, updatedJson);
            return recordIdExists;
        }
    }
}
