using webapi.Domain.ValueObjects;

namespace webapi.Domain.Entities;

public class Pizza : BaseEntity
{
    private readonly List<Ingrediente> _ingredientes = new();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public ImageUrl ImageUrl { get; private set; }
    public IReadOnlyList<Ingrediente> Ingredientes => _ingredientes.AsReadOnly();

    /// <summary>
    /// Calcula el precio de la pizza como la suma del costo de todos los ingredientes más un 20% de beneficio
    /// </summary>
    public Money Price 
    { 
        get 
        { 
            var costoTotal = _ingredientes.Aggregate(Money.Zero, (acc, ingrediente) => acc + ingrediente.Costo);
            return costoTotal * 1.20m; // Suma del costo + 20% de beneficio
        } 
    }

    private Pizza() : base()
    {
        Name = string.Empty;
        Description = string.Empty;
        ImageUrl = ImageUrl.Create("https://example.com/default.jpg");
    }

    private Pizza(int id, string name, string description, string imageUrl) : base(id)
    {
        SetName(name);
        SetDescription(description);
        SetImageUrl(imageUrl);
    }

    public static Pizza Create(int id, string name, string description, string imageUrl)
    {
        return new Pizza(id, name, description, imageUrl);
    }

    public void UpdateName(string name)
    {
        SetName(name);
    }

    public void UpdateDescription(string description)
    {
        SetDescription(description);
    }

    public void UpdateImageUrl(string imageUrl)
    {
        SetImageUrl(imageUrl);
    }

    public void AddIngrediente(Ingrediente ingrediente)
    {
        if (ingrediente == null)
            throw new ArgumentNullException(nameof(ingrediente));

        if (_ingredientes.Any(i => i.Id == ingrediente.Id))
            throw new InvalidOperationException($"El ingrediente '{ingrediente.Name}' ya está agregado a la pizza");

        _ingredientes.Add(ingrediente);
    }

    public void RemoveIngrediente(int ingredienteId)
    {
        var ingrediente = _ingredientes.FirstOrDefault(i => i.Id == ingredienteId);
        if (ingrediente == null)
            throw new InvalidOperationException($"El ingrediente con ID {ingredienteId} no se encuentra en la pizza");

        _ingredientes.Remove(ingrediente);
    }

    public void ClearIngredientes()
    {
        _ingredientes.Clear();
    }

    public bool HasIngrediente(int ingredienteId)
    {
        return _ingredientes.Any(i => i.Id == ingredienteId);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de la pizza no puede estar vacío", nameof(name));

        if (name.Length > 100)
            throw new ArgumentException("El nombre de la pizza no puede tener más de 100 caracteres", nameof(name));

        Name = name.Trim();
    }

    private void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("La descripción de la pizza no puede estar vacía", nameof(description));

        if (description.Length > 500)
            throw new ArgumentException("La descripción de la pizza no puede tener más de 500 caracteres", nameof(description));

        Description = description.Trim();
    }

    private void SetImageUrl(string imageUrl)
    {
        ImageUrl = ImageUrl.Create(imageUrl);
    }
}