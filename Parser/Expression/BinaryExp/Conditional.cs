namespace Hulk;

public abstract class ConditionalExpression : BinaryExpression
{
    public ConditionalExpression(Expression left, Expression right) : base(left, right) { }
}

public class AndExpression : ConditionalExpression
{
    public AndExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override string Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Left.Value! == (double)Right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class OrExpression : ConditionalExpression
{
    public OrExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override string Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Left.Value! == 1 || (double)Right.Value! == 1 ? 1 : 0;
        return Value.ToString()!;
    }
}

public class NotExpresion : UnaryExpression
{
    public NotExpresion(Expression arg) : base(arg) { }

    public override string Evaluate()
    {
        return Arg.Evaluate() == "true" ? "false" : "true";
    }
}

public class DistintExpression : ConditionalExpression
{
    public DistintExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override string Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Left.Value! != (double)Right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class EqualExpression : ConditionalExpression
{
    public EqualExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override string Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Left.Value! == (double)Right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class MenorExpression : ConditionalExpression
{
    public MenorExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override string Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Left.Value! < (double)Right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class MayorExpression : ConditionalExpression
{
    public MayorExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override string Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Left.Value! > (double)Right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class MenorEqualExpression : ConditionalExpression
{
    public MenorEqualExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override string Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Left.Value! <= (double)Right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class MayorEqualExpression : ConditionalExpression
{
    public MayorEqualExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override string Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (double)Left.Value! >= (double)Right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}