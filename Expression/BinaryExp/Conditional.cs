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

    public override bool CheckSemantic(List<Errors> errors)
    {
        bool Right = right!.CheckSemantic(errors);
        bool Left = left!.CheckSemantic(errors);
        if (right.Type != ExpressionType.Boolean || left.Type != ExpressionType.Boolean)
        {
            errors.Add(new Errors(ErrorCode.Semantic, "No se puede efectuar la operacion con" + left.Value + " y " + right.Value));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return Right && Left;
    }

    public override string Evaluate()
    {
        Value = (left.Evaluate() == "true") && (right.Evaluate() == "true") ? 1 : 0;

        if ((int)Value == 1)
            return "true";

        return "false";
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


    public override bool CheckSemantic(List<Errors> errors)
    {
        bool Right = right!.CheckSemantic(errors);
        bool Left = left!.CheckSemantic(errors);
        if (right.Type != ExpressionType.Boolean || left.Type != ExpressionType.Boolean)
        {
            errors.Add(new Errors(ErrorCode.Semantic, "No se puede efectuar la operacion con" + left.Value + " y " + right.Value));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return Right && Left;
    }

    public override string Evaluate()
    {
        Value = (left!.Evaluate() == "true") || (right.Evaluate() == "true") ? 1 : 0;

        if ((int)Value == 1)
            return "true";

        return "false";
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


    public override bool CheckSemantic(List<Errors> errors)
    {
        return Arg.CheckSemantic(errors);
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

    public override bool CheckSemantic(List<Errors> errors)
    {
        bool Right = right!.CheckSemantic(errors);
        bool Left = left!.CheckSemantic(errors);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, "No se puede efectuar la operacion con" + left.Value + " y " + right.Value));
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


        Value = (double)(Convert.ToInt32(left!.Evaluate()) > Convert.ToInt32(right!.Evaluate()) ? 1 : 0);

        if ((int)Value == 1)
            return "true";

        return "false";
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


    public override bool CheckSemantic(List<Errors> errors)
    {
        bool Right = right!.CheckSemantic(errors);
        bool Left = left!.CheckSemantic(errors);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, "No se puede efectuar la operacion con" + left.Value + " y " + right.Value));
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


        Value = (double)(Convert.ToInt32(right.Value!) == Convert.ToInt32(left.Value!) ? 1 : 0);

        if (Convert.ToInt32(Value) == 1)
            return "true";

        return "false";
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


    public override bool CheckSemantic(List<Errors> errors)
    {
        bool Right = right!.CheckSemantic(errors);
        bool Left = left!.CheckSemantic(errors);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, "No se puede efectuar la operacion con" + left.Value + " y " + right.Value));
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


        Value = (double)(Convert.ToInt32(left!.Evaluate()) < Convert.ToInt32(right!.Evaluate()) ? 1 : 0);
        if ((int)Value == 1)
            return "true";

        return "false";
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


    public override bool CheckSemantic(List<Errors> errors)
    {
        bool Right = right!.CheckSemantic(errors);
        bool Left = left!.CheckSemantic(errors);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, "No se puede efectuar la operacion con" + left.Value + " y " + right.Value));
            Type = ExpressionType.ErrorType;
            return false;
        }

        Type = ExpressionType.Boolean;
        return Right && Left;
    }

    public override string Evaluate()
    {
        Value = (double)(Convert.ToInt32(left!.Evaluate()) > Convert.ToInt32(right!.Evaluate()) ? 1 : 0);

        if (Convert.ToInt32(Value) == 1)
            return "true";

        return "false";
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


    public override bool CheckSemantic(List<Errors> errors)
    {
        bool Right = right!.CheckSemantic(errors);
        bool Left = left!.CheckSemantic(errors);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, "No se puede efectuar la operacion con" + left.Value + " y " + right.Value));
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


        Value = (double)(Convert.ToInt32(left!.Evaluate()) <= Convert.ToInt32(right!.Evaluate()) ? 1 : 0);

        if ((int)Value == 1)
            return "true";

        return "false";
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


    public override bool CheckSemantic(List<Errors> errors)
    {
        bool Right = right!.CheckSemantic(errors);
        bool Left = left!.CheckSemantic(errors);
        if (right.Type != ExpressionType.Number || left.Type != ExpressionType.Number)
        {
            errors.Add(new Errors(ErrorCode.Semantic, "No se puede efectuar la operacion con" + left.Value + " y " + right.Value));
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


        Value = (double)(Convert.ToInt32(left!.Evaluate()) >= Convert.ToInt32(right!.Evaluate()) ? 1 : 0);

        if ((int)Value == 1)
            return "true";

        return "false";
    }
}