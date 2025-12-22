namespace WarehouseInventory.Api.Dtos
{
    public record CreateProductRequestDto(
        string Name,
        string SKU,
        decimal Price,
        int Quantity,
        int CategoryId
    );
}
