namespace Home_Accounting.Models
{
    public class CategoryType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
