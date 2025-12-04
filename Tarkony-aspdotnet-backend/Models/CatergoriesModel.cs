using System.Collections.Generic;
    public class CategoriesRoot
    {
        public CategoriesModel Data { get; set; }
    }
    public partial class CategoriesModel
    {
        public List<Categories> ItemCategories { get; set; }
    }
    public partial class Categories
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public Parent Parent { get; set; }
        public List<Parent> Children { get; set; }
    }

    public partial class Parent
    {
        public string NormalizedName { get; set; }
    }