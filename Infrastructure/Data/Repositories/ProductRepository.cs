using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbContext.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _dbContext.Products.Remove(product);
    }
}