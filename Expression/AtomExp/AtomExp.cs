
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
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope) { }

    public override bool CheckSemantic(List<Errors> errors)
    {
        Type = ExpressionType.Number;
        return true;
    }

    public override string Evaluate()
    {
        return Value!.ToString()!;
    }
}
public class Text : AtomExpression
{
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope) { }

    public override bool CheckSemantic(List<Errors> errors)
    {
        Type = ExpressionType.Text;
        return true;
    }

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
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope) { }

    public override bool CheckSemantic(List<Errors> errors)
    {
        Type = ExpressionType.Boolean;
        return true;
    }

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
    private Scope? scope;
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        if (scope.Parent == null)
        {
            Type = ExpressionType.ErrorType;
            return;
        }

        foreach (var item in scope.Parent!.Type.Keys)
        {
            if (item.value == id.value)
            {
                this.scope = scope.Parent;
                return;
            }
        }

        Scope(scope.Parent);
    }
    public override bool CheckSemantic(List<Errors> errors)
    {
        foreach (var item in scope!.Type.Keys)
        {
            if (item.value == id.value)
            {
                Type = scope.Type[item];
                return true;
            }
        }

        errors.Add(new Errors(ErrorCode.Semantic, "Esta variable no esta declarada"));
        return false;
    }

    public override string Evaluate()
    {
        foreach (var item in scope!.Value.Keys)
        {
            if (item.value == id.value)
            {
                Value = scope.Value[item];
                return Value.ToString()!;
            }

        }
        throw new();
    }
}

public class Chain_Lietarls : AtomExpression
{
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }
    public string value;

    public Chain_Lietarls(string value)
    {
        this.value = value;
    }
    public override bool CheckSemantic(List<Errors> errors)
    {
        Type = ExpressionType.Text;
        return true;
    }

    public override string Evaluate()
    {
        return value;
    }

    public override void Scope(Scope scope) { }
}