namespace Categories
{
    using System.Collections.Generic;
/**
    In the menu used as category flat tree
*/
    public class CategoriesRoot
    {
        public CategoryList Data { get; set; }
    }
    public class CategoryList
    {
        public List<CategoryModel> ItemCategories { get; set; }
    }
    public class CategoryModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public Parent Parent { get; set; }
        public List<Parent> Children { get; set; }
    }

    public class Parent
    {
        public string NormalizedName { get; set; }
    }
}