using System;
using System.Collections.Generic;
using System.Text;

using Application.Interfaces;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public IProductRepository Products { get; }
    public IAuthRepository Auth { get; }

    public UnitOfWork(
        ApplicationDbContext context,
        IProductRepository productRepository, IAuthRepository auth)
    {
        _context = context;
        Products = productRepository;
        Auth = auth;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
