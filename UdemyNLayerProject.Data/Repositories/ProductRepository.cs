using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Repositories;
using UdemyNLayerProject.Data.Context;

namespace UdemyNLayerProject.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public ProductRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Product> GetWithCategoryByIdAsync(int id)
        {
            return await _appDbContext.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}