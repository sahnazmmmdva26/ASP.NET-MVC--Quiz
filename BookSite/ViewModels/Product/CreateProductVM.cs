using System.ComponentModel.DataAnnotations;

namespace BookSite.ViewModels
{
    public class CreateProductVM
    {

        public string Name { get; set; }
        [Range(0.0, double.MaxValue)]
        public double Price { get; set; }
        public IFormFile CoverImage { get; set; }
        public IFormFile? HoverImage { get; set; }
        public ICollection<IFormFile>? OtherImages { get; set; }
    }
}
