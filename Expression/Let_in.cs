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
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        Scope L = new Scope(scope, new(), new());
        Let.Scope(L);

        Scope i = new Scope(scope, new(), new());
        In.Scope(i);
    }

    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        if (!(Let.CheckSemantic(errors, scope) && In.CheckSemantic(errors, scope)))
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        return true;
    }

    public override string Evaluate()
    {
        Let.Evaluate();
        return In.Evaluate();
    }
}

public class AssingExpression : Expression
{
    public AssingExpression(Token id, Expression value)
    {
        this.id = id;
        this.value = value;
    }

    public Token id;
    public Expression value;
    Scope scope;
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        scope.Type.Add(id, ExpressionType.AnyType);
        value.Scope(scope);
        this.scope = scope;
    }

    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        foreach (var item in this.scope.Type.Keys)
        {
            if (item.value == id.value)
            {
                value.CheckSemantic(errors,this.scope);
                this.scope.Type[item] = value.Type;
                Type = this.scope.Type[item];
                return true;
            }
        }

        errors.Add(new Errors(ErrorCode.Semantic, ""));
        return false;
    }

    public override string Evaluate()
    {
        foreach (var item in scope.Type.Keys)
        {
            if (item.value == id.value)
            {
                string result = value.Evaluate();
                scope.Value.Add(id,result);
            }
        }

        throw new();
    }
}