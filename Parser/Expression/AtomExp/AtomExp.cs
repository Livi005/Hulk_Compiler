namespace Hulk;

public abstract class AtomExpression : Expression
{

}

public class Number : AtomExpression
{
    public Number(double value)
    {
        Value = value;
    }
    public override object? Value { get; set; }

    public override string Evaluate() 
    {
        return Value!.ToString()!;
     }
}
public class Text : AtomExpression
{
    public override object? Value { get; set; }

    public override string Evaluate() 
    {
        return Value!.ToString()!;
     }
}
public class Bool : AtomExpression
{
    public Bool(bool value)
    {
        Value = value;
    }
    public override object? Value { get; set; }

    public override string Evaluate() 
    {
        return Value!.ToString()!;
     }
}
public class ID : AtomExpression
{
    public ID(Token id, Expression value)
    {
        this.id = id;
        Value = value;
    }
    public Token id;
    public override object? Value { get; set; }

    public override string Evaluate() 
    {
        return Value!.ToString()!;
     }
}