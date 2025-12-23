namespace WarehouseInventory.Api.Dtos;

public record UpdateProductRequestDto(
     string Name,
    decimal Price,
    int Quantity
);
