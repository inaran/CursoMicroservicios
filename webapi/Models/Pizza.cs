namespace webapi.Models;

public class Pizza
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public List<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
    
    /// <summary>
    /// Calcula el precio de la pizza como la suma del costo de todos los ingredientes más un 20% de beneficio
    /// </summary>
    public decimal Price 
    { 
        get 
        { 
            var costoIngredientes = Ingredientes.Sum(i => i.Costo);
            return costoIngredientes * 1.20m; // Suma del costo + 20% de beneficio
        } 
    }
}