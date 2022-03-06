namespace Domain;

public record Id<T> (Guid Value)
{
    public override string ToString() => Value.ToString();
}
