namespace Hulk;
public class Tokenizar
{
    public List<Token> Salida;
    public int index;
    public string TokenValue;

    public List<Token> Analizar(string cadena,List<Errors> error)
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
                GetNumber(i);
                i = index;
            }
            else if (Char.IsLetter(Leyendo))
            {
                GetText(i);
                i = index;
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

                GetSymbol(i);
                i = index;
            }

        }
        AddToken(TokenName.EOF);

        void GetText(int index)
        {
            for (int i = index + 1; i < cadena.Length; i++)
            {
                if (Char.IsLetterOrDigit(Leyendo))
                {
                    Leyendo = cadena.ElementAt(i);
                    TokenValue += Leyendo;
                    index++;

                    if (TokenValue.CompareTo("in") == 0)
                    {
                        AddToken(TokenName.In);
                    }
                    else if (TokenValue.CompareTo("let") == 0)
                    {
                        AddToken(TokenName.let);
                    }
                    else if (TokenValue.CompareTo("if") == 0)
                    {
                        AddToken(TokenName.If);
                    }
                    else if (TokenValue.CompareTo("else") == 0)
                    {
                        AddToken(TokenName.Else);
                    }
                    else if (TokenValue.CompareTo("them") == 0)
                    {
                        AddToken(TokenName.them);
                    }
                    else if (TokenValue.CompareTo("log") == 0)
                    {
                        AddToken(TokenName.log);
                    }
                    else if (TokenValue.CompareTo("squart") == 0)
                    {
                        AddToken(TokenName.squart);
                    }
                    else if (TokenValue.CompareTo("function") == 0)
                    {
                        AddToken(TokenName.function);
                    }
                    else if (TokenValue.CompareTo("PI") == 0)
                    {
                        AddToken(TokenName.PI);
                    }
                    else if (TokenValue.CompareTo("print") == 0)
                    {
                        AddToken(TokenName.print);
                    }
                    else if (TokenValue.CompareTo("var") == 0)
                    {
                        AddToken(TokenName.var);
                    }
                }

                else
                {
                    AddToken(TokenName.text);
                    break;
                }
            }
        }


        void GetNumber(int index)
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
                       error.Add(new Errors(ErrorCode.Invalid, Leyendo.ToString()));                             
                }
                else
                    AddToken(TokenName.number);

            }

        }


        void GetSymbol(int index)
        {
            for (int i = index; i < cadena.Length; i++)
            {
                Leyendo = cadena.ElementAt(i);
                TokenValue += Leyendo;

                if (Leyendo.CompareTo('+') == 0)
                    AddToken(TokenName.plus);

                else if (Leyendo.CompareTo('-') == 0)
                    AddToken(TokenName.sub);

                else if (Leyendo.CompareTo('*') == 0)
                    AddToken(TokenName.star);

                else if (Leyendo.CompareTo('/') == 0)
                    AddToken(TokenName.div);

                else if (Leyendo.CompareTo('^') == 0)
                    AddToken(TokenName.pow);

                else if (Leyendo.CompareTo('%') == 0)
                    AddToken(TokenName.mod);

                else if (Leyendo.CompareTo('\"') == 0)
                    AddToken(TokenName.comilla);

                else if (Leyendo.CompareTo('(') == 0)
                    AddToken(TokenName.openP);

                else if (Leyendo.CompareTo(')') == 0)
                    AddToken(TokenName.closeP);

                else if (Leyendo.CompareTo('{') == 0)
                    AddToken(TokenName.openL);

                else if (Leyendo.CompareTo('}') == 0)
                    AddToken(TokenName.closeL);

                else if (Leyendo.CompareTo(',') == 0)
                    AddToken(TokenName.coma);

                else if (Leyendo.CompareTo('&') == 0)
                    AddToken(TokenName.and);

                else if (Leyendo.CompareTo('|') == 0)
                    AddToken(TokenName.or);

                else if (Leyendo.CompareTo('<') == 0)
                {
                    if (cadena[i + 1].CompareTo('=') == 0)
                    {
                        TokenValue += cadena[i + 1];
                        AddToken(TokenName.MenorEqual);
                        i++;
                    }
                    else AddToken(TokenName.menor);
                }
                else if (Leyendo.CompareTo('>') == 0)
                {
                    if (cadena[i + 1].CompareTo('=') == 0)
                    {
                        TokenValue += cadena[i + 1];
                        AddToken(TokenName.MayorEqual);
                        i++;
                    }
                    else AddToken(TokenName.mayor);
                }
                else if (Leyendo.CompareTo('!') == 0)
                {
                    if (cadena[i + 1].CompareTo('=') == 0)
                    {
                        TokenValue += cadena[i + 1];
                        AddToken(TokenName.distint);
                        i++;
                    }
                    error.Add(new Errors(ErrorCode.Invalid, Leyendo.ToString()));            
                }
                else if (Leyendo.CompareTo(':') == 0)
                {
                    if (cadena[i + 1].CompareTo('=') == 0)
                    {
                        TokenValue += cadena[i + 1];
                        AddToken(TokenName.pointEqualToken);
                        i++;
                    }
                    error.Add(new Errors(ErrorCode.Invalid, Leyendo.ToString())); 
                }
                else if (Leyendo.CompareTo(';') == 0)
                    AddToken(TokenName.end);

                else
                    error.Add(new Errors(ErrorCode.Unknown, Leyendo.ToString()));                  
            }

        }

        return Salida;
    }



    private void AddToken(TokenName tipo)
    {
        if (tipo == TokenName.plus)
        {
            Salida.Add(new PlusToken());
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
