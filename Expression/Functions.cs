namespace Hulk;

public class Print : Expression
{
    public Expression print;
    public Print(Expression print)
    {
        this.print = print;
    }

    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        print.Scope(scope);
    }

    public override bool CheckSemantic(List<Errors> errors)
    {
        return print.CheckSemantic(errors);
    }

    public override string Evaluate()
    {
        return print.Evaluate();
    }
}
public class Function : Expression
{
    public string Name;
    public List<Token> Arguments;
    public Expression Instructions;
    public Scope scope = new Scope(Context.General, new(), new());

    public Function(string Name, List<Token> Arguments, Expression Instructions)
    {
        this.Name = Name;
        this.Arguments = Arguments;
        this.Instructions = Instructions;
    }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        foreach (var item in Arguments)
            scope.Type.Add(item, ExpressionType.AnyType);

        Scope s = new Scope(scope, new(), new());
        Instructions.Scope(s);
        this.scope = scope;
    }

    public override bool CheckSemantic(List<Errors> errors)
    {
        Type = ExpressionType.AnyType;
        return true;
    }

    public override string Evaluate()
    {
        throw new NotImplementedException();
    }
}

public class FuncCall : Expression
{
    public FuncCall(List<Expression> expressions, Function f)
    {
        exp = expressions;
        func = f;
    }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }
    Function func;
    List<Expression> exp;

    public override bool CheckSemantic(List<Errors> errors)
    {
        if (func.Arguments.Count == exp.Count)
        {
            foreach (var item in exp)
                item.CheckSemantic(errors);

            Type = ExpressionType.AnyType;
            return true;
        }

        Type = ExpressionType.ErrorType;
        return false;
    }

    public override string Evaluate()
    {
        Dictionary<Token, string> dicc = func.scope.Value;
        Dictionary<Token, string> Arguments = new();
        int index = 0;

        foreach (var item in exp)
        {
            Arguments.Add(func.Arguments[index], item.Evaluate());
            index++;
        }

        func.scope.Value = Arguments;
        string result = func.Instructions.Evaluate();
        func.scope.Value = dicc;

        return result;
    }

    public override void Scope(Scope scope)
    {
        foreach (var item in exp)
        {
            item.Scope(scope);
        }
        return;
    }
}