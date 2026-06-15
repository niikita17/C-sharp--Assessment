using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class ProductService:IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponseDto>CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow
            };

            await _unitOfWork.Products
                .AddAsync(product);

            await _unitOfWork.SaveChangesAsync();

            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CreatedBy = product.CreatedBy,
                CreatedOn = product.CreatedOn
            };
        }

        public async Task DeleteAsync(int id)
        {
            var product= await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException("product not found");
           
                 _unitOfWork.Products.Delete(product);
                await _unitOfWork.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var products =
                await _unitOfWork.Products.GetAllAsync();

            return products.Select(p =>
                new ProductResponseDto
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn
                });
        }

        public async Task<ProductResponseDto?>
    GetByIdAsync(int id)
        {
            var product =
                await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException(
                    "Product not found");

            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CreatedBy = product.CreatedBy,
                CreatedOn = product.CreatedOn
            };
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            product.ProductName = dto.ProductName;
            product.ModifiedBy = "System";
            product.ModifiedOn = DateTime.UtcNow;

            _unitOfWork.Products.Update(product);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
