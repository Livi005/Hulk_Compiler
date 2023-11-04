namespace Hulk;
public class Print : Expression
{
    public Expression print;
    public Print(Expression print)
    {
        this.print = print;
    }

    public override object? Value { get; set; }

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

    public Function(string Name,List<Token> Arguments,Expression Instructions)
    {
        this.Name = Name;
        this.Arguments = Arguments;
        this.Instructions = Instructions;
    }
    public override object? Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override string Evaluate()
    {
        throw new NotImplementedException();
    }
}