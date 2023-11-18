namespace Hulk;
public class If_them_else : Expression
{
    public Expression If;
    public Expression Them;
    public Expression Else;

    public If_them_else(Expression If, Expression Them, Expression Else)
    {
        this.If = If;
        this.Them = Them;
        this.Else = Else;
    }
    public override object? Value { get; set; }

    public override string Evaluate()
    {
       return If.Evaluate() == "true" ? Them.Evaluate() : Else.Evaluate();
    }
}