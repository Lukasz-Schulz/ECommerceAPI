using System;
using System.Collections.Generic;
using System.IO;

namespace ECommerceApp.Models
{
    /// <summary>
    ///Holds a dictionary of availible categories. 
    /// </summary>
    public class CategoryCollection
    {
        public Dictionary<string, Category> AvailibleCategories { get; private set; } = new Dictionary<string, Category>();
        readonly string _categoryLocation = @"Resources\categories.txt";
        public CategoryCollection()
        { 
            string categories;
            using (StreamReader reader = new StreamReader(_categoryLocation))
            {
                categories = reader.ReadToEnd();
            }
            string[] categoryArray = categories.Split(' ');
            foreach(var category in categoryArray)
            {
                AvailibleCategories.Add(category, new Category());
            }
        }
        //public Dictionary<string, Category> AvailibleCategories { get; private set; } = new Dictionary<string, Category> {


        //    { "hemelektronik", new Category()},
        //    { "sport", new Category()},
        //    {"leksaker", new Category()}
        //};
    }
}
