namespace Hulk;

public abstract class NoConditionalExpression : BinaryExpression
{
    public NoConditionalExpression(Expression left, Expression right) : base(left, right) { }
}

public class Add : NoConditionalExpression
{
    public Add(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Right.Value! + (double)Left.Value!;
    }
}
public class Sub : NoConditionalExpression
{
    public Sub(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Right.Value! - (double)Left.Value!;
    }
}
public class Start : NoConditionalExpression
{
    public Start(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Right.Value! * (double)Left.Value!;
    }

}
public class Div : NoConditionalExpression
{
    public Div(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Right.Value! / (double)Left.Value!;
    }

}
public class Pow : NoConditionalExpression
{
    public Pow(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();

        Value = (int)Right.Value! ^ (int)Left.Value!;
    }

}
public class Mod : NoConditionalExpression
{
    public Mod(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }

    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Right.Value! % (double)Left.Value!;
    }

}