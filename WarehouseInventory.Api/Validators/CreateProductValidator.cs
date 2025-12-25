using FluentValidation;
using WarehouseInventory.Api.Dtos;

public class CreateProductValidator : AbstractValidator<CreateProductRequestDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(x => x.SKU)
            .Matches(@"^WH-\d{4}$")
            .WithMessage("SKU must follow format WH-1234.");
    }
}
