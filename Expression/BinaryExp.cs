namespace Hulk;

public abstract class BinaryExpression : Expression
{
    public Expression? right { get; set; }
    public Expression? left { get; set; }

    public BinaryExpression(Expression left, Expression right)
    {
        this.left = left; 
        this.right = right;
    }
}

