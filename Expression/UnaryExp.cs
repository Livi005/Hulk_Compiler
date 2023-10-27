namespace Hulk;

public abstract class UnaryExpression : Expression
{

}

public class Sen : UnaryExpression
{
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        throw new NotImplementedException();
    }
}
public class Cos : UnaryExpression
{
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        throw new NotImplementedException();
    }
}
public class Tan : UnaryExpression
{
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        throw new NotImplementedException();
    }
}
public class Log : UnaryExpression
{
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        throw new NotImplementedException();
    }
}
public class Squart : UnaryExpression
{
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        throw new NotImplementedException();
    }
}