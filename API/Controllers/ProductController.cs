using Application.DTOs;
using Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[ApiVersion("1.0")]


[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(
        IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products =
            await _productService.GetAllAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product =
            await _productService.GetByIdAsync(id);

        return Ok(product);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductDto dto)
    {
        var product =
            await _productService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product);
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateProductDto dto)
    {
        await _productService.UpdateAsync(
            id,
            dto);

        return NoContent();
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        await _productService.DeleteAsync(id);

        return NoContent();
    }
}
