namespace Tokenizer;
public enum TokenType
{
    plus,star,sub
}

public abstract class Token //Clase abstracta Token, crear token para cada tokentype
{
    TokenType type;
    string value;

    public Token(TokenType type,string value)
    {
        this.type = type;
        this.value = value;
    }
}


