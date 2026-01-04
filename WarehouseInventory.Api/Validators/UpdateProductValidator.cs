using FluentValidation;
using WarehouseInventory.Api.Dtos;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequestDto>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(x => x.Price)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Quantity)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantity cannot be negative.");
    }
}
