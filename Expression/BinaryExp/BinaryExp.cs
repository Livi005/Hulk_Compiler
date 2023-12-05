namespace Hulk;

public abstract class BinaryExpression : Expression
{
    public Expression? Right { get; set; }
    public Expression? Left { get; set; }

    public BinaryExpression(Expression left, Expression right)
    {
        Left = left; 
        Right = right;
    }
}

