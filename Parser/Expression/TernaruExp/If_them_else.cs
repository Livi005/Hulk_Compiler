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
    public override ExpressionType Type { get; set; }


    public override void Scope(Scope scope)
    {
        If!.Scope(scope);
        Them!.Scope(scope);
        Else.Scope(scope);
    }


    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool IF = If!.CheckSemantic(errors, scope);
        bool THEM = Them!.CheckSemantic(errors, scope);
        bool ELSE = Else!.CheckSemantic(errors, scope);

        if (If.Type != ExpressionType.Boolean)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return IF && THEM && ELSE;
    }

    public override string Evaluate()
    {
        return If.Evaluate() == "true" ? Them.Evaluate() : Else.Evaluate();
    }
}