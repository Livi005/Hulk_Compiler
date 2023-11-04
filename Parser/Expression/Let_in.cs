namespace Hulk;
public class LetInExpression : Expression
{
    Expression Let;
    Expression In;

    public LetInExpression(Expression let, Expression In)
    {
        this.Let = let;
        this.In = In;
    }
    public override object? Value { get; set; }

    public override string Evaluate()
    {
        Let.Evaluate();
        return In.Evaluate();
    }
}

public class AssingExpression : Expression
{
    Token t;
    Expression value;

    public AssingExpression(Token t, Expression value)
    {
        this.t = t;
        this.value = value;
    }

    public override object? Value { get; set; }

    public override string Evaluate()
    {
        throw new NotImplementedException();                    ///////////////////////////////////////////////////////////////////
    }
}