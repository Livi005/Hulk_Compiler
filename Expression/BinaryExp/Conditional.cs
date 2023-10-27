namespace Hulk;

public abstract class ConditionalExpression : BinaryExpression
{
    public ConditionalExpression(Expression left, Expression right) : base(left, right) { }
}

public class AndExpression : ConditionalExpression
{
    public AndExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (int)Left.Value! == (int)Right.Value! ? 1 : 0;
    }
}
public class OrExpression : ConditionalExpression
{
    public OrExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (int)Left.Value! == 1 || (int)Right.Value! == 1 ? 1 : 0;
    }
}
public class DistintExpression : ConditionalExpression
{
    public DistintExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (int)Left.Value! != (int)Right.Value! ? 1 : 0;
    }
}
public class EqualExpression : ConditionalExpression
{
    public EqualExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (int)Left.Value! == (int)Right.Value! ? 1 : 0;
    }
}
public class MenorExpression : ConditionalExpression
{
    public MenorExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (int)Left.Value! < (int)Right.Value! ? 1 : 0;
    }
}
public class MayorExpression : ConditionalExpression
{
    public MayorExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (int)Left.Value! > (int)Right.Value! ? 1 : 0;
    }
}
public class MenorEqualExpression : ConditionalExpression
{
    public MenorEqualExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (int)Left.Value! <= (int)Right.Value! ? 1 : 0;
    }
}
public class MayorEqualExpression : ConditionalExpression
{
    public MayorEqualExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override void Evaluate()
    {
        Right!.Evaluate();
        Left!.Evaluate();


        Value = (int)Left.Value! >= (int)Right.Value! ? 1 : 0;
    }
}