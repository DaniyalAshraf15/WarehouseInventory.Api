namespace WarehouseInventory.Api.Dtos;

public record ProductResponseDto(
    int Id,
    string Name,
    string SKU,
    decimal Price,
    int Quantity,
    int CategoryId
);
