namespace webapi.Application.DTOs;

public record PizzaDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public List<IngredienteDto> Ingredientes { get; init; } = new();
}

public record IngredienteDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Costo { get; init; }
}

public record CreatePizzaDto
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public List<int> IngredienteIds { get; init; } = new();
}

public record UpdatePizzaDto
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
    public List<int>? IngredienteIds { get; init; }
}