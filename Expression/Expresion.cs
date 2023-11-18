namespace Hulk;

public abstract class Expression
{
    public abstract object? Value { get; set; }
    public abstract ExpressionType Type{get; set;}

    public abstract void Scope(Scope scope);
    public abstract bool CheckSemantic(List<Errors> errors, Context context, Scope scope);
    public abstract string Evaluate();
}

public enum ExpressionType
{
    Text,
    Number,
    ID,
    Aritmetic,
    Boolean,
    ErrorType,
    AnyType
}