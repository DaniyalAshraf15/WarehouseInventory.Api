namespace WarehouseInventory.Api.Dtos;

public record UpdateProductRequestDto(
    decimal Price,
    int Quantity
);
