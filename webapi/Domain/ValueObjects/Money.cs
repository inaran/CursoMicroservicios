namespace webapi.Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; }

    private Money(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("El monto no puede ser negativo", nameof(amount));
        
        Amount = amount;
    }

    public static Money Create(decimal amount) => new(amount);

    public static Money Zero => new(0);

    public static Money operator +(Money left, Money right) => new(left.Amount + right.Amount);
    
    public static Money operator -(Money left, Money right) => new(left.Amount - right.Amount);
    
    public static Money operator *(Money money, decimal multiplier) => new(money.Amount * multiplier);

    public static implicit operator decimal(Money money) => money.Amount;

    public override string ToString() => $"${Amount:F2}";
}