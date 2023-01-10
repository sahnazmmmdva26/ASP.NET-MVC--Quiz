using BookSite.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace BookSite.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
