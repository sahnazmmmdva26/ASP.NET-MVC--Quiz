using BookSite.DAL;
using BookSite.Models;
using BookSite.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookSite.Utilies;

namespace BookSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {

            return View(_context.Products.Include(p => p.ProductImages));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [HttpPost]
        public IActionResult Create(CreateProductVM cp)
        {
            var coverimg = cp.CoverImage;
            var hoverimg = cp.HoverImage;
            var otherimg = cp.OtherImages;


            if (hoverimg?.CheckType("image/") == false)
            {
                ModelState.AddModelError("Hover", "yuklediyiniz fayl shekil deyil");
            }
            if (hoverimg?.CheckSize(500) == false)
            {
                ModelState.AddModelError("Hover", "yuklediyiniz faylin olcusu 500kb dan az olmalidi");
            }

            if (coverimg?.CheckType("image/") == false)
            {
                ModelState.AddModelError("Hover", "yuklediyiniz fayl shekil deyil");
            }
            if (coverimg?.CheckSize(500) == false)
            {
                ModelState.AddModelError("Hover", "yuklediyiniz faylin olcusu 500kb dan az olmalidi");
            }
            if (!ModelState.IsValid)
            { 
                return View();
            }
            Product newProduct = new Product
            {
                Name = cp.Name,
                Price = cp.Price,
                IsDeleted = false
            };
            List<ProductImage> images = new List<ProductImage>();
            images.Add(new ProductImage
            {
                ImageUrl = coverimg.SaveFile(Path.Combine(_env.WebRootPath, "book-shop",
                "img", "product")),
                IsCover = true,
                Product = newProduct
            });
            foreach (var item in otherimg)
            {
                if (item?.CheckType("image/") == false)
                {
                    ModelState.AddModelError("Hover", "yuklediyiniz fayl shekil deyil");
                }
                if (item?.CheckSize(500) == false)
                {
                    ModelState.AddModelError("Hover", "yuklediyiniz faylin olcusu 500kb dan az olmalidi");
                }
                images.Add(new ProductImage
                {
                    ImageUrl = item.SaveFile(Path.Combine(_env.WebRootPath, "book-shop",
              "img", "product")),
                    IsCover = null,
                    Product = newProduct
                });
            }

            newProduct.ProductImages = images;
            _context.Products.Add(newProduct);
           
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult UpdateList(int? id)
        {
            if (id == null) return BadRequest();
            Product product = new Product();

            product = _context.Products.Find(id);
            if (product is null) return NotFound();


            return View(product);
        }
        public IActionResult Update(int key)
        {
            ViewBag.Key = key;
            return View();
        }

        [HttpPost]
        public IActionResult Update(int? id,CreateProductVM cp)
        {
            if (id == null) return BadRequest();
            if (cp is null) return NotFound();
            Product product = _context.Products.Find(id);
            product.Name=cp.Name;
            product.Price = cp.Price;
            foreach (ProductImage item in cp.OtherImages)
            {
                product.ProductImages.Add(item);
            }
            //ardini bilemedim :(
            return RedirectToAction(nameof(Index));
        }





        public IActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();
            Product exist=_context.Products.FirstOrDefault(p => p.Id == id); 
            if (exist == null) return NotFound();
            exist.IsDeleted = true;
            exist.Name.DeleteFile(_env.WebRootPath,"book-shop/img/product");
            _context.Products.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }
    }
}
