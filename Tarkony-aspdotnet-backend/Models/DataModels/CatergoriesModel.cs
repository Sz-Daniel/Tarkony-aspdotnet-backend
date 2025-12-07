namespace Categories
{
    /**
        In the menu used as category flat tree
    */
    public class Root
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
        public List<Normalized_Name> Children { get; set; }
        public Normalized_Name Parent { get; set; }
    }

    public class Normalized_Name
    {
        public string NormalizedName { get; set; }
    }
}
