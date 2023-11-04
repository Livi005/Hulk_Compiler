namespace Hulk;

public abstract class UnaryExpression : Expression
{
    public override object? Value { get; set; }
    public Expression Arg { get; set; }

    public UnaryExpression(Expression arg)
    {
        Arg = arg;
    }
}
public class Sen : UnaryExpression
{
    public Sen(Expression arg) : base(arg) { }

    public override string Evaluate()
    {
        Value = Math.Sin(double.Parse(Arg.Evaluate()));
        return Value.ToString()!;
    }
}
public class Cos : UnaryExpression
{
    public Cos(Expression arg) : base(arg) { }

    public override string Evaluate()
    {
        Value = Math.Cos(double.Parse(Arg.Evaluate()));
        return Value.ToString()!;
    }
}
public class Tan : UnaryExpression
{
    public Tan(Expression arg) : base(arg) { }

    public override string Evaluate()
    {
        Value = Math.Tan(double.Parse(Arg.Evaluate()));
        return Value.ToString()!;
    }
}
public class Log : UnaryExpression
{
    public Log(Expression arg) : base(arg) { }

    public override string Evaluate()
    {
        Value = Math.Log(double.Parse(Arg.Evaluate()));
        return Value.ToString()!;
    }
}
public class Squart : UnaryExpression
{
    public Squart(Expression arg) : base(arg) { }

    public override string Evaluate()
    {
        Value = Math.Sqrt(double.Parse(Arg.Evaluate()));
        return Value.ToString()!;
    }
}