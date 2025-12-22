using WarehouseInventory.Api.Models;
using WarehouseInventory.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


var products = new List<Product>
{
    new Product
    {
        Id = 1,
        Name = "Laptop",
        SKU = "WH-1001",
        Price = 1200,
        Quantity = 10,
        CategoryId = 1
    },
    new Product
    {
        Id = 2,
        Name = "Mouse",
        SKU = "WH-1002",
        Price = 25,
        Quantity = 100,
        CategoryId = 1
    }
};


var productsGroup = app.MapGroup("/products");


// GET /products
productsGroup.MapGet("/", () =>
{
    var response = products.Select(p =>
        new ProductResponseDto(
            p.Id,
            p.Name,
            p.SKU,
            p.Price,
            p.Quantity,
            p.CategoryId
        ));

    return Results.Ok(response);
});


// GET /products/{id}
productsGroup.MapGet("/{id:int}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);

    if (product is null)
        return Results.NotFound();

    var response = new ProductResponseDto(
        product.Id,
        product.Name,
        product.SKU,
        product.Price,
        product.Quantity,
        product.CategoryId
    );

    return Results.Ok(response);
});


// POST /products
productsGroup.MapPost("/", (CreateProductRequestDto request) =>
{
    var newProduct = new Product
    {
        Id = products.Max(p => p.Id) + 1,
        Name = request.Name,
        SKU = request.SKU,
        Price = request.Price,
        Quantity = request.Quantity,
        CategoryId = request.CategoryId
    };

    products.Add(newProduct);

    var response = new ProductResponseDto(
        newProduct.Id,
        newProduct.Name,
        newProduct.SKU,
        newProduct.Price,
        newProduct.Quantity,
        newProduct.CategoryId
    );

    return Results.Created($"/products/{newProduct.Id}", response);
});


// PUT /products/{id}
productsGroup.MapPut("/{id:int}", (int id, UpdateProductRequestDto request) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);

    if (product is null)
        return Results.NotFound();

    product.Price = request.Price;
    product.Quantity = request.Quantity;

    return Results.NoContent();
});



// DELETE /products/{id}
productsGroup.MapDelete("/{id:int}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);

    if (product is null)
        return Results.NotFound();

    products.Remove(product);
    return Results.NoContent();
});


app.Run();
