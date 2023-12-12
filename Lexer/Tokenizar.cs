namespace Hulk;
public class Tokenizar
{
    string cadena;
    List<Errors> error;
    public Tokenizar(string cadena, List<Errors> error)
    {
        this.cadena = cadena;
        this.error = error;
    }
    public List<Token> Salida;
    public int index;
    public string TokenValue;


    public List<Token> Analizar()
    {
        cadena = cadena + "#";
        Salida = new List<Token>();
        index = 0;
        TokenValue = "";
        char Leyendo;

        for (int i = 0; i < cadena.Length - 1; i++)
        {
            Leyendo = cadena.ElementAt(i);
            if (Char.IsNumber(Leyendo))
            {
                i = GetNumber(i);
            }
            else if (Char.IsLetter(Leyendo))
            {
                i = GetText(i);
            }
            else if (Leyendo.CompareTo(' ') == 0)
            {
                continue;
            }
            else if (Leyendo.CompareTo('#') == 0)
            {
                break;
            }
            else
            {
                if (Salida.Count > 0 && Salida[Salida.Count - 1].name == TokenName.text)
                {
                    Salida[Salida.Count - 1].name = TokenName.id;
                }

                i = GetSymbol(i);
            }

        }
        AddToken(TokenName.EOF);

        int GetText(int index)
        {
            for (int i = index; i < cadena.Length; i++)
            {
                if (Char.IsLetterOrDigit(Leyendo))
                {
                    Leyendo = cadena.ElementAt(i);
                    TokenValue += Leyendo;

                    if (TokenValue.CompareTo("in") == 0)
                    {
                        AddToken(TokenName.In);
                        break;
                    }
                    else if (TokenValue.CompareTo("let") == 0)
                    {
                        AddToken(TokenName.let);
                        break;
                    }
                    else if (TokenValue.CompareTo("if") == 0)
                    {
                        AddToken(TokenName.If);
                        break;
                    }
                    else if (TokenValue.CompareTo("else") == 0)
                    {
                        AddToken(TokenName.Else);
                        break;
                    }
                    else if (TokenValue.CompareTo("them") == 0)
                    {
                        AddToken(TokenName.them);
                        break;
                    }
                    else if (TokenValue.CompareTo("true") == 0)
                    {
                        AddToken(TokenName.True);
                        break;
                    }
                    else if (TokenValue.CompareTo("false") == 0)
                    {
                        AddToken(TokenName.False);
                        break;
                    }
                    else if (TokenValue.CompareTo("not") == 0)
                    {
                        AddToken(TokenName.not);
                        break;
                    }
                    if (TokenValue.CompareTo("sen") == 0)
                    {
                        AddToken(TokenName.sen);
                        break;
                    }
                    if (TokenValue.CompareTo("cos") == 0)
                    {
                        AddToken(TokenName.cos);
                        break;
                    }
                    if (TokenValue.CompareTo("tan") == 0)
                    {
                        AddToken(TokenName.tan);
                        break;
                    }
                    else if (TokenValue.CompareTo("log") == 0)
                    {
                        AddToken(TokenName.log);
                        break;
                    }
                    else if (TokenValue.CompareTo("squart") == 0)
                    {
                        AddToken(TokenName.squart);
                        break;
                    }
                    else if (TokenValue.CompareTo("function") == 0)
                    {
                        AddToken(TokenName.function);
                        break;
                    }
                    else if (TokenValue.CompareTo("PI") == 0)
                    {
                        AddToken(TokenName.PI);
                        break;
                    }
                    else if (TokenValue.CompareTo("print") == 0)
                    {
                        AddToken(TokenName.print);
                        break;
                    }
                    else if (TokenValue.CompareTo("var") == 0)
                    {
                        AddToken(TokenName.var);
                        break;
                    }
                    else if (cadena[i + 1].CompareTo(' ') == 0 || (!Char.IsLetter(cadena[i + 1]) && !Char.IsLetter(cadena[i + 1])))
                    {
                        AddToken(TokenName.id);
                        break;
                    }
                    else if (cadena[i + 1].CompareTo(' ') == 0 && (!Char.IsLetter(cadena[i + 2]) && !Char.IsLetter(cadena[i + 2])))
                    {
                        AddToken(TokenName.id);
                        break;
                    }
                    else if (cadena[i + 1].CompareTo(' ') == 0)
                    {
                        AddToken(TokenName.text);
                        break;
                    }
                    index = i;
                    break;
                }
            }
            return index;
        }


        int GetNumber(int index)
        {
            TokenValue += Leyendo;
            for (int i = index + 1; i < cadena.Length; i++)
            {
                Leyendo = cadena.ElementAt(i);

                if (Char.IsNumber(Leyendo))
                {
                    index++;
                    TokenValue += Leyendo;
                }
                else if (Leyendo.CompareTo('.') == 0)
                {
                    if (Char.IsNumber(cadena[i + 1]))
                    {
                        index++;
                        TokenValue += Leyendo;
                    }
                    else
                    {
                        error.Add(new Errors(ErrorCode.Lexer, Leyendo.ToString()));
                        index = i;
                        break;
                    }
                }
                else
                {
                    AddToken(TokenName.number);
                    index = i;
                    break;
                }
            }
            return --index;
        }

        int GetSymbol(int index)
        {
            for (int i = index; i < cadena.Length;)
            {
                Leyendo = cadena.ElementAt(i);
                TokenValue += Leyendo;

                if (Leyendo.CompareTo('+') == 0)
                {
                    AddToken(TokenName.add);
                    break;
                }
                else if (Leyendo.CompareTo('-') == 0)
                {
                    AddToken(TokenName.sub);
                    break;

                }
                else if (Leyendo.CompareTo('*') == 0)
                {
                    AddToken(TokenName.star);
                    break;
                }
                else if (Leyendo.CompareTo('/') == 0)
                {
                    AddToken(TokenName.div);
                    break;
                }
                else if (Leyendo.CompareTo('^') == 0)
                {
                    AddToken(TokenName.pow);
                    break;
                }
                else if (Leyendo.CompareTo('%') == 0)
                {
                    AddToken(TokenName.mod);
                    break;
                }
                else if (Leyendo.CompareTo('\"') == 0)
                {
                    AddToken(TokenName.comilla);
                    break;
                }
                else if (Leyendo.CompareTo('(') == 0)
                {
                    AddToken(TokenName.openP);
                    break;
                }
                else if (Leyendo.CompareTo(')') == 0)
                {
                    AddToken(TokenName.closeP);
                    break;
                }
                else if (Leyendo.CompareTo('{') == 0)
                {
                    AddToken(TokenName.openL);
                    break;
                }
                else if (Leyendo.CompareTo('}') == 0)
                {
                    AddToken(TokenName.closeL);
                    break;
                }
                else if (Leyendo.CompareTo(',') == 0)
                {
                    AddToken(TokenName.coma);
                    break;
                }
                else if (Leyendo.CompareTo('&') == 0)
                {
                    AddToken(TokenName.and);
                    break;
                }
                else if (Leyendo.CompareTo('|') == 0)
                {
                    AddToken(TokenName.or);
                    break;
                }
                else if (Leyendo.CompareTo('=') == 0)
                {
                    if (cadena[i + 1].CompareTo('=') == 0)
                    {
                        TokenValue += cadena[i + 1];

                        AddToken(TokenName.EqualEqual);
                        index = i + 1;
                    }
                    else if(cadena[i + 1].CompareTo('>') == 0)
                    {
                        TokenValue += cadena[i + 1];

                        AddToken(TokenName.implic);
                        index = i + 1;
                    }
                    else
                    {
                        AddToken(TokenName.equal);
                        index = i;
                    }

                    break;
                }
                else if (Leyendo.CompareTo('<') == 0)
                {
                    if (cadena[i + 1].CompareTo('=') == 0)
                    {
                        TokenValue += cadena[i + 1];
                        AddToken(TokenName.MenorEqual);
                        index = i + 1;
                    }
                    else
                    {
                        AddToken(TokenName.menor);
                        index = i;
                    }
                    break;
                }
                else if (Leyendo.CompareTo('>') == 0)
                {
                    if (cadena[i + 1].CompareTo('=') == 0)
                    {
                        TokenValue += cadena[i + 1];
                        AddToken(TokenName.MayorEqual);
                        index = i + 1;
                        break;
                    }
                    else AddToken(TokenName.mayor);
                    index = i;
                    break;
                }
                else if (Leyendo.CompareTo('!') == 0)
                {
                    if (cadena[i + 1].CompareTo('=') == 0)
                    {
                        TokenValue += cadena[i + 1];
                        AddToken(TokenName.distint);
                        index = i + 1;
                        break;
                    }
                    error.Add(new Errors(ErrorCode.Lexer, Leyendo.ToString()));
                    index = i;
                    break;
                }
                else if (Leyendo.CompareTo(':') == 0)
                {
                    if (cadena[i + 1].CompareTo('=') == 0)
                    {
                        TokenValue += cadena[i + 1];
                        AddToken(TokenName.pointEqualToken);
                        index = i;
                        break;
                    }
                    error.Add(new Errors(ErrorCode.Lexer, Leyendo.ToString()));
                    index = i;
                    break;
                }
                else if (Leyendo.CompareTo(';') == 0)
                {
                    AddToken(TokenName.end);
                    index = i;
                    break;
                }
                else
                    error.Add(new Errors(ErrorCode.Lexer, Leyendo.ToString()));

                index = i;
                break;
            }
            return index;
        }

        return Salida;
    }

    private void AddToken(TokenName tipo)
    {
        if (tipo == TokenName.add)
        {
            Salida.Add(new AddToken());
        }
        else if (tipo == TokenName.sub)
        {
            Salida.Add(new SubToken());
        }
        else if (tipo == TokenName.star)
        {
            Salida.Add(new StarToken());
        }
        else if (tipo == TokenName.div)
        {
            Salida.Add(new DivToken());
        }
        else if (tipo == TokenName.pow)
        {
            Salida.Add(new PowToken());
        }
        else if (tipo == TokenName.mod)
        {
            Salida.Add(new ModToken());
        }
        if (tipo == TokenName.comilla)
        {
            Salida.Add(new ComillaToken());
        }
        else if (tipo == TokenName.openL)
        {
            Salida.Add(new OpenLToken());
        }
        else if (tipo == TokenName.openP)
        {
            Salida.Add(new OpenPToken());
        }
        else if (tipo == TokenName.closeL)
        {
            Salida.Add(new CloseLToken());
        }
        else if (tipo == TokenName.closeP)
        {
            Salida.Add(new ClosePToken());
        }
        else if (tipo == TokenName.equal)
        {
            Salida.Add(new EqualToken());
        }
        if (tipo == TokenName.menor)
        {
            Salida.Add(new MenorToken());
        }
        if (tipo == TokenName.mayor)
        {
            Salida.Add(new MayorToken());
        }
        if (tipo == TokenName.exc)
        {
            Salida.Add(new ExcToken());
        }
        else if (tipo == TokenName.or)
        {
            Salida.Add(new OrToken());
        }
        else if (tipo == TokenName.and)
        {
            Salida.Add(new AndToken());
        }
        else if (tipo == TokenName.coma)
        {
            Salida.Add(new ComaToken());
        }
        else if (tipo == TokenName.MenorEqual)
        {
            Salida.Add(new MenorEqualToken());
        }
        else if (tipo == TokenName.MayorEqual)
        {
            Salida.Add(new MayorEqualToken());
        }
        else if (tipo == TokenName.EqualEqual)
        {
            Salida.Add(new EqualEqualToken());
        }
        else if (tipo == TokenName.distint)
        {
            Salida.Add(new DistintToken());
        }
        else if (tipo == TokenName.implic)
        {
            Salida.Add(new ImplicToken());
        }
        else if (tipo == TokenName.pointEqualToken)
        {
            Salida.Add(new PointEqualToken());
        }
        else if (tipo == TokenName.number)
        {
            Salida.Add(new NumberToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.text)
        {
            Salida.Add(new TextToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.id)
        {
            Salida.Add(new IdToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.sen)
        {
            Salida.Add(new SenToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.cos)
        {
            Salida.Add(new CosToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.tan)
        {
            Salida.Add(new TanToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.In)
        {
            Salida.Add(new InToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.let)
        {
            Salida.Add(new LetToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.If)
        {
            Salida.Add(new IfToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.Else)
        {
            Salida.Add(new ElseToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.them)
        {
            Salida.Add(new ThemToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.True)
        {
            Salida.Add(new TrueToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.False)
        {
            Salida.Add(new FalseToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.not)
        {
            Salida.Add(new NotToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.squart)
        {
            Salida.Add(new SquartToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.log)
        {
            Salida.Add(new LogToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.function)
        {
            Salida.Add(new FunctionToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.PI)
        {
            Salida.Add(new PiToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.print)
        {
            Salida.Add(new PrintToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.var)
        {
            Salida.Add(new VarToken(tipo, TokenValue));
        }
        else if (tipo == TokenName.end)
        {
            Salida.Add(new EndToken());
        }
        else if (tipo == TokenName.EOF)
        {
            Salida.Add(new EOFToken());
        }

        TokenValue = "";
    }

    public void Imprimir(List<Token> list)
    {
        foreach (Token item in list)
        {
            System.Console.WriteLine(item.GetType() + " --> " + item.GetValue());
        }
    }
}
