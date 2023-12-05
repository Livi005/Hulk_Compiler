namespace Hulk;

public abstract class NoConditionalExpression : BinaryExpression
{
    public NoConditionalExpression(Expression left, Expression right) : base(left, right) { }

}
public class Concat : BinaryExpression
{
    public Concat(Expression left, Expression right) : base(left, right) { }

    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        var Left = left!.CheckSemantic(errors, scope);
        var Right = right!.CheckSemantic(errors, scope);
        if (right.Type != ExpressionType.Text || left.Type != ExpressionType.Text)
        {
            errors.Add(new Errors(ErrorCode.Semantic, "operacion invalida"));
            Type = ExpressionType.ErrorType;
            return false;

        }

        Type = ExpressionType.Text;
        return Left && Right;

    }

    public override string Evaluate()
    {
        return left!.Evaluate() + right!.Evaluate();
    }

    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }
}

public class Add : NoConditionalExpression
{
    public Add(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }


    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, scope);
        bool Left = left!.CheckSemantic(errors, scope);

        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, "no se puede operar con objetos q no son de tipo numero"));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)right.Value! + (double)left.Value!;
        return Value.ToString()!;

    }
}
public class Sub : NoConditionalExpression
{
    public Sub(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }

    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, scope);
        bool Left = left!.CheckSemantic(errors, scope);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)right.Value! - (double)left.Value!;
        return Value.ToString()!;
    }
}
public class Start : NoConditionalExpression
{
    public Start(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }

    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, scope);
        bool Left = left!.CheckSemantic(errors, scope);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)right.Value! * (double)left.Value!;
        return Value.ToString()!;
    }

}
public class Div : NoConditionalExpression
{
    public Div(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }

    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, scope);
        bool Left = left!.CheckSemantic(errors, scope);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)right.Value! / (double)left.Value!;
        return Value.ToString()!;
    }

}
public class Pow : NoConditionalExpression
{
    public Pow(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }

    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, scope);
        bool Left = left!.CheckSemantic(errors, scope);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (int)right.Value! ^ (int)left.Value!;
        return Value.ToString()!;
    }

}
public class Mod : NoConditionalExpression
{
    public Mod(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }

    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }

    public override bool CheckSemantic(List<Errors> errors, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, scope);
        bool Left = left!.CheckSemantic(errors, scope);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Number;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)right.Value! % (double)left.Value!;
        return Value.ToString()!;
    }

}