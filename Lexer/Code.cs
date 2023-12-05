namespace Hulk;
//Code: clase moverse con mayor comodidad en la lista de tokens, entre sus tipos y valores.
public class Code 
{
    public List<Token> tokens{get;}
    public int position;
    public int Position { get { return position; } }

    public Code(IEnumerable<Token> tokens)
    {
        this.tokens = new List<Token>(tokens);
        position = 0;
    }

    public bool End => position == tokens.Count - 1;

    public void MoveNext(int k)
    {
        position += k;
    }

    public void MoveBack(int k)
    {
        position -= k;
    }
    public bool Next()
    {
        if (position < tokens.Count - 1)
        {
            position++;
        }

        return position < tokens.Count;
    }

    public bool Next(TokenName name)
    {
        if (position < tokens.Count - 1 && LookAhead(1).name == name)
        {
            position++;
            return true;
        }

        return false;
    }

    public bool Next(string value)
    {
        if (position < tokens.Count - 1 && LookAhead(1).value == value)
        {
            position++;
            return true;
        }

        return false;
    }

    public bool CanLookAhead(int k = 0)
    {
        return tokens.Count - position > k;
    }

    public Token LookAhead(int k = 0)
    {
        return tokens[position + k];
    }

    public IEnumerator<Token> GetEnumerator()
    {
        for (int i = position; i < tokens.Count; i++)
            yield return tokens[i];
    }

    // IEnumerator IEnumerable.GetEnumerator()
    // {
    //     return GetEnumerator();
    // }
}

