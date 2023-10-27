namespace Hulk;

public abstract class AtomExpression : UnaryExpression
{

}

public class Number : AtomExpression
{
    public override object? Value { get; set; }

    public override void Evaluate() { }
}

public class Text : AtomExpression
{
    public override object? Value { get; set; }

    public override void Evaluate() { }
}