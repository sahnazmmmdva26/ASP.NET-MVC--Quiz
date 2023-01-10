namespace BookSite.Models
{
    public class Product
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
    }
}
