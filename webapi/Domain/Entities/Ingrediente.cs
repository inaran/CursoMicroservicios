using webapi.Domain.ValueObjects;

namespace webapi.Domain.Entities;

public class Ingrediente : BaseEntity
{
    public string Name { get; private set; }
    public Money Costo { get; private set; }

    private Ingrediente() : base() 
    {
        Name = string.Empty;
        Costo = Money.Zero;
    }

    private Ingrediente(int id, string name, Money costo) : base(id)
    {
        SetName(name);
        SetCosto(costo);
    }

    public static Ingrediente Create(int id, string name, decimal costo)
    {
        return new Ingrediente(id, name, Money.Create(costo));
    }

    public void UpdateName(string name)
    {
        SetName(name);
    }

    public void UpdateCosto(decimal costo)
    {
        SetCosto(Money.Create(costo));
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del ingrediente no puede estar vacío", nameof(name));

        if (name.Length > 100)
            throw new ArgumentException("El nombre del ingrediente no puede tener más de 100 caracteres", nameof(name));

        Name = name.Trim();
    }

    private void SetCosto(Money costo)
    {
        Costo = costo ?? throw new ArgumentNullException(nameof(costo));
    }
}