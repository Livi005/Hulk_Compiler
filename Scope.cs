namespace Hulk;

public class Scope
{
    public Scope? Parent;
    public Dictionary<Token, string> Value;
    public Dictionary<Token, ExpressionType> Type;

    public Scope(Scope parent, Dictionary<Token, string> Value, Dictionary<Token, ExpressionType> Type)
    {
        Parent = parent;
        this.Value = Value;
        this.Type = Type;
    }

    // public Scope CreateChild()
    // {
    //     Scope child = new Scope(this);

    //     return child;
    // }
}