using WarehouseInventory.Api.Dtos;
namespace WarehouseInventory.Api.Tests.Unit
{

    public class ProductValidatorTests
    {
        private readonly CreateProductValidator _validator;

        public ProductValidatorTests()
        {
            _validator = new CreateProductValidator();
        }

        [Fact]
        public void Should_Fail_When_Name_Is_Empty()
        {
            var dto = new CreateProductRequestDto(
                Name: "",
                SKU: "WH-1114",
                Price: 100,
                Quantity: 5,
                CategoryId: 1
            );

            var result = _validator.Validate(dto);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors,
                e => e.ErrorMessage == "Product name is required.");
        }

        [Fact]
        public void Should_Pass_When_Data_Is_Valid()
        {
            var dto = new CreateProductRequestDto(
                Name: "Laptop",
                SKU: "WH-1234",
                Price: 100,
                Quantity: 5,
                CategoryId: 1
            );

            var result = _validator.Validate(dto);

            Assert.True(result.IsValid);
        }
    }
}
