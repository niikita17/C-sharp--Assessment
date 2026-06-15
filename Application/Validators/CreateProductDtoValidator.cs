using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateProductDtoValidator
    : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MaximumLength(255);

      
    }
}