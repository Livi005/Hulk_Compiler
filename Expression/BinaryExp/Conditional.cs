namespace Hulk;

public abstract class ConditionalExpression : BinaryExpression
{
    public ConditionalExpression(Expression left, Expression right) : base(left, right) { }
}

public class AndExpression : ConditionalExpression
{
    public AndExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }
    
    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }

    public override bool CheckSemantic(List<Errors> errors, Context context, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, context, scope);
        bool Left = left!.CheckSemantic(errors, context, scope);
        if (right.Type != ExpressionType.Boolean || left.Type != ExpressionType.Boolean)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)left.Value! == (double)right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class OrExpression : ConditionalExpression
{
    public OrExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }
    
    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }


    public override bool CheckSemantic(List<Errors> errors, Context context, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, context, scope);
        bool Left = left!.CheckSemantic(errors, context, scope);
        if (right.Type != ExpressionType.Boolean || left.Type != ExpressionType.Boolean)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)left.Value! == 1 || (double)right.Value! == 1 ? 1 : 0;
        return Value.ToString()!;
    }
}

public class NotExpresion : UnaryExpression
{
    public NotExpresion(Expression arg) : base(arg) { }

    public override ExpressionType Type { get; set; }
    
    public override void Scope(Scope scope)
    {
        Arg!.Scope(scope);
    }


    public override bool CheckSemantic(List<Errors> errors, Context context, Scope scope)
    {
        throw new Exception();
    }

    public override string Evaluate()
    {
        return Arg.Evaluate() == "true" ? "false" : "true";
    }
}

public class DistintExpression : ConditionalExpression
{
    public DistintExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }
    
    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }


    public override bool CheckSemantic(List<Errors> errors, Context context, Scope scope)
    {
        throw new NotImplementedException();
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)left.Value! != (double)right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class EqualExpression : ConditionalExpression
{
    public EqualExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }
    
    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }


    public override bool CheckSemantic(List<Errors> errors, Context context, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, context, scope);
        bool Left = left!.CheckSemantic(errors, context, scope);
        if (right.Type != ExpressionType.Boolean || left.Type != ExpressionType.Boolean)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)left.Value! == (double)right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class MenorExpression : ConditionalExpression
{
    public MenorExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }
    
    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }


    public override bool CheckSemantic(List<Errors> errors, Context context, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, context, scope);
        bool Left = left!.CheckSemantic(errors, context, scope);
        if (right.Type != ExpressionType.Boolean || left.Type != ExpressionType.Boolean)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)left.Value! < (double)right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class MayorExpression : ConditionalExpression
{
    public MayorExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }
    
    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }


    public override bool CheckSemantic(List<Errors> errors, Context context, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, context, scope);
        bool Left = left!.CheckSemantic(errors, context, scope);
        if (right.Type != ExpressionType.Boolean || left.Type != ExpressionType.Boolean)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)left.Value! > (double)right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class MenorEqualExpression : ConditionalExpression
{
    public MenorEqualExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }
    
    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }


    public override bool CheckSemantic(List<Errors> errors, Context context, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, context, scope);
        bool Left = left!.CheckSemantic(errors, context, scope);
        if (right.Type != ExpressionType.Boolean || left.Type != ExpressionType.Boolean)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)left.Value! <= (double)right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}
public class MayorEqualExpression : ConditionalExpression
{
    public MayorEqualExpression(Expression left, Expression right) : base(left, right) { }
    public override object? Value { get; set; }
    public override ExpressionType Type { get; set; }
    
    public override void Scope(Scope scope)
    {
        left!.Scope(scope);
        right!.Scope(scope);
    }


    public override bool CheckSemantic(List<Errors> errors, Context context, Scope scope)
    {
        bool Right = right!.CheckSemantic(errors, context, scope);
        bool Left = left!.CheckSemantic(errors, context, scope);
        if (right.Type != ExpressionType.Boolean || left.Type != ExpressionType.Boolean)
        {
            errors.Add(new Errors(ErrorCode.Semantic, ""));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return Right && Left;
    }

    public override string Evaluate()
    {
        right!.Evaluate();
        left!.Evaluate();


        Value = (double)left.Value! >= (double)right.Value! ? 1 : 0;
        return Value.ToString()!;
    }
}