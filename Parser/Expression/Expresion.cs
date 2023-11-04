namespace Hulk;

public abstract class Expression
{
    public abstract object? Value { get; set; }
    public abstract string Evaluate();
}