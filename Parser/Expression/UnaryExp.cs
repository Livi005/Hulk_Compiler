namespace Hulk;

public abstract class UnaryExpression : Expression
{
    public override object? Value { get; set; }
    public Expression Arg { get; set; }

    public UnaryExpression(Expression arg)
    {
        Arg = arg;
    }
}
public class Sen : UnaryExpression
{
    public Sen(Expression arg) : base(arg) { }

    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        Arg!.Scope(scope);
    }

    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool arg = Arg!.CheckSemantic(errors, scope);
        if (Arg.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return arg;
    }

    public override string Evaluate()
    {
        Value = Math.Sin(double.Parse(Arg.Evaluate()));
        return Value.ToString()!;
    }
}
public class Cos : UnaryExpression
{
    public Cos(Expression arg) : base(arg) { }

    public override ExpressionType Type { get; set; }
    
    public override void Scope(Scope scope)
    {
        Arg!.Scope(scope);
    }
    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool arg = Arg!.CheckSemantic(errors, scope);
        if (Arg.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return arg;
    }

    public override string Evaluate()
    {
        Value = Math.Cos(double.Parse(Arg.Evaluate()));
        return Value.ToString()!;
    }
}
public class Tan : UnaryExpression
{
    public Tan(Expression arg) : base(arg) { }

    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        Arg!.Scope(scope);
    }
    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool arg = Arg!.CheckSemantic(errors, scope);
        if (Arg.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return arg;
    }

    public override string Evaluate()
    {
        Value = Math.Tan(double.Parse(Arg.Evaluate()));
        return Value.ToString()!;
    }
}
public class Log : UnaryExpression
{
    public Log(Expression arg) : base(arg) { }

    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        Arg!.Scope(scope);
    }
    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool arg = Arg!.CheckSemantic(errors, scope);
        if (Arg.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return arg;
    }

    public override string Evaluate()
    {
        Value = Math.Log(double.Parse(Arg.Evaluate()));
        return Value.ToString()!;
    }
}
public class Squart : UnaryExpression
{
    public Squart(Expression arg) : base(arg) { }

    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        Arg!.Scope(scope);
    }
    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool arg = Arg!.CheckSemantic(errors, scope);
        if (Arg.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return arg;
    }

    public override string Evaluate()
    {
        Value = Math.Sqrt(double.Parse(Arg.Evaluate()));
        return Value.ToString()!;
    }
}