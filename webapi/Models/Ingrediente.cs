namespace webapi.Models;

public class Ingrediente
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Costo { get; set; }
}