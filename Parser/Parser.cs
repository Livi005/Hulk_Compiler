namespace Hulk;

public class Parser
{
    public Parser(List<Token> code)
    {
        this.code = code;
        donde_me_quede = 0;
    }

    //Lista de tokens 
    public int donde_me_quede { get; }
    public List<Token> code { get; }

    public bool IsFunc;

    //Las lineas de codigo solo deben empesar con las palabras print, if, let,function o un numero
    //Lo primero que hace es preguntar cual de esas palabras es la primera y automaticamente llama al parser q corresponda

    public Expression ParserCode(List<Errors> error, int position, Expression epsilon = null!)
    {
        if (code[position].name == TokenName.EOF) return epsilon;

        if (code[position].name == TokenName.print)
        {
            Expression result = ParserCode(error, position + 1, epsilon);
            Expression print = new Print(result);

            if (!Context.IsDeclare_Func)
                IsFunc = true;

            return print;
        }

        if (code[position].name == TokenName.let)
        {
            if (code[position + 1].name == TokenName.id)
            {
                Token id = code[position + 1];
                if (code[position + 2].name == TokenName.equal)
                {
                    Expression result = ParserCode(error, position + 3, epsilon);
                    Expression asig = new AssingExpression(id, result);
                    epsilon = asig;
                    return ParserB(error, epsilon, position);
                }

                error.Add(new Errors(ErrorCode.Sintaxis, "detras del nombre de una variable debe ir un ="));
            }

            error.Add(new Errors(ErrorCode.Sintaxis, "debe declararse una variable poniendose el id"));
        }

        if (code[position].name == TokenName.function && position == 0)
        {
            if (code[position + 1].name == TokenName.id)
            {
                Token func_name = code[position + 1];
                if (code[position + 2].name == TokenName.openP)
                {
                    List<Token> param = ParserID(error, new(), position + 3);
                    if (code[position].name == TokenName.id)                                         //!.................................
                    {
                        Context.IsDeclare_Func = true;
                        Function func = new Function(func_name.value, param, null!);
                        Context.funcs.Add(func);

                        Expression instrutcions = ParserCode(error, position + 1);
                        func.Instructions = instrutcions;

                        return func;
                    }
                    error.Add(new Errors(ErrorCode.Sintaxis, code[position].value));
                }
                error.Add(new Errors(ErrorCode.Sintaxis, " Le falto: " + code[position].value));
            }
            error.Add(new Errors(ErrorCode.Sintaxis, "se le debe poner un identificador a la funcion"));
        }

        if (code[position].name == TokenName.If)
        {
            Expression If = ParserCode(error, position + 1, epsilon);
            Expression Them = ParserCode(error, position, If);

            if (code[position].name == TokenName.Else)
            {
                Expression Else = ParserCode(error, position + 1, Them);
                Expression ITE = new If_them_else(If, Them, Else);

                return ITE;
            }

            error.Add(new Errors(ErrorCode.Sintaxis, "Le falto el else"));
        }

        return ParserD(error, position, epsilon);
    }
    //B --> in A | e
    public Expression ParserB(List<Errors> error, Expression epsilon, int position)
    {
        if (code[position].name == TokenName.In)
            code.Insert(position + 1, new Token(TokenName.let, "let", TokenType.Keyword));

        else
        {
            error.Add(new Errors(ErrorCode.Sintaxis, "le falto el in"));
            return epsilon;
        }
        Expression result = ParserCode(error, position + 1, epsilon);
        Expression Let_in = new LetInExpression(epsilon, result);
        return Let_in;
    }
    //ID --> id | B| e
    public List<Token> ParserID(List<Errors> error, List<Token> ID, int position)
    {
        if (code[position].name == TokenName.id)
        {
            ID.Add(code[position]);
            return ParserID(error, ID, position + 1);
        }

        if (code[position].name == TokenName.closeP)
            return ID;

        error.Add(new Errors(ErrorCode.Sintaxis, code[position].value));
        return null!;
    }
    //H --> E | H | e
    public List<Expression> ParserH(List<Errors> error, List<Expression> exp, int position)
    {
        for (int i = position; i < code.Count; i++)                 //i comienza en el token en el que estoy
        {
            var expression = ParserCode(error, i);
            exp.Add(expression);
            i = position;
            if (code[position + 1].name == TokenName.closeP)
            {
                return exp;
            }
        }

        error.Add(new Errors(ErrorCode.Sintaxis, code[position].value));
        return null!;
    }
    //D --> I And_Or
    public Expression ParserD(List<Errors> error, int position, Expression epsilon = null!)
    {
        Expression result;
        if (code[position].name == TokenName.not)
        {
            Expression exp = ParserI(error, epsilon, position + 1);
            Expression Not = new NotExpresion(exp);
            result = ParserAnd_Or(error, Not, position);
        }
        else
        {
            Expression exp = ParserI(error, epsilon, position);
            result = ParserAnd_Or(error, exp, position);
        }
        return result;
    }
    //And_Or --> and A | or A | e
    public Expression ParserAnd_Or(List<Errors> error, Expression epsilon, int position)
    {
        Expression result;

        if (code[position].name == TokenName.and)
        {
            result = ParserD(error, position + 1, epsilon);
            Expression Add = new AndExpression(epsilon, result);
            return Add;
        }
        if (code[position].name == TokenName.or)
        {
            result = ParserD(error, position + 1, epsilon);
            Expression Sub = new OrExpression(epsilon, result);
            return Sub;
        }

        return epsilon;
    }
    //I --> EC 
    public Expression ParserI(List<Errors> error, Expression epsilon, int position)
    {
        Expression E = ParserFactor(error, epsilon, position);
        Expression C = ParserY(error, E, position);

        return C;
    }
    //C --> >E | <E | >=E | <=E | ==E | !E | e
    public Expression ParserComparer(List<Errors> error, Expression epsilon, int position)
    {
        if (code[position].name == TokenName.mayor)
        {
            Expression result = ParserExpression(error, epsilon, position);
            Expression mayor = new MayorExpression(epsilon, result);
            return mayor;
        }
        if (code[position].name == TokenName.menor)
        {
            Expression result = ParserExpression(error, epsilon, position);
            Expression menor = new MenorExpression(epsilon, result);
            return menor;
        }
        if (code[position].name == TokenName.MayorEqual)
        {
            Expression result = ParserExpression(error, epsilon, position);
            Expression MayorEqual = new MayorEqualExpression(epsilon, result);
            return MayorEqual;
        }
        if (code[position].name == TokenName.MenorEqual)
        {
            Expression result = ParserExpression(error, epsilon, position);
            Expression MenorEqual = new MenorEqualExpression(epsilon, result);
            return MenorEqual;
        }
        if (code[position].name == TokenName.distint)
        {
            Expression result = ParserExpression(error, epsilon, position);
            Expression distint = new DistintExpression(epsilon, result);
            return distint;
        }
        if (code[position].name == TokenName.EqualEqual)
        {
            Expression result = ParserExpression(error, epsilon, position);
            Expression EqualEqual = new EqualExpression(epsilon, result);
            return EqualEqual;
        }

        return epsilon;
    }
    //E --> TX
    public Expression ParserExpression(List<Errors> error, Expression epsilon, int position)
    {
        Expression T = ParserTerm(error, epsilon, position);
        Expression X = ParserX(error, T, position);

        return X;
    }
    //X --> +E | -E | e
    public Expression ParserX(List<Errors> error, Expression epsilon, int position)
    {
        Expression result;

        if (code[position].name == TokenName.add)
        {
            result = ParserExpression(error, epsilon, position + 1);
            Expression Add = new Add(epsilon, result);
            return Add;
        }
        if (code[position].name == TokenName.sub)
        {
            result = ParserExpression(error, epsilon, position + 1);
            Expression Sub = new Sub(epsilon, result);
            return Sub;
        }
        if (code[position].name == TokenName.concat)
        {
            result = ParserExpression(error, epsilon, position + 1);
            Expression Concat = new Concat(epsilon, result);
            return Concat;
        }

        return epsilon;
    }
    //T --> FY
    public Expression ParserTerm(List<Errors> error, Expression epsilon, int position)
    {
        Expression F = ParserFactor(error, epsilon, position);
        Expression Y = ParserY(error, F, position);

        return Y;
    }
    //Y --> *E | /E | %E | ^E | e
    public Expression ParserY(List<Errors> error, Expression epsilon, int position)
    {
        Expression result;

        if (code[position].name == TokenName.star)
        {
            result = ParserExpression(error, epsilon, position + 1);
            Expression Mult = new Start(epsilon, result);
            return Mult;
        }
        if (code[position].name == TokenName.div)
        {
            result = ParserExpression(error, epsilon, position + 1);
            Expression Div = new Div(epsilon, result);
            return Div;
        }
        if (code[position].name == TokenName.mod)
        {
            result = ParserExpression(error, epsilon, position + 1);
            Expression Mod = new Mod(epsilon, result);
            return Mod;
        }
        if (code[position].name == TokenName.pow)
        {
            result = ParserExpression(error, epsilon, position + 1);
            Expression Pow = new Pow(epsilon, result);
            return Pow;
        }
        return epsilon;
    }
    //F --> int | (A) | true | false | id | Func(H) | @-Concat | 
    public Expression ParserFactor(List<Errors> error, Expression epsilon, int position)
    {

        if (code[position].name == TokenName.sen)
        {
            Expression result = ParserExpression(error, epsilon, position + 1);
            return new Sen(result);
        }
        if (code[position].name == TokenName.cos)
        {
            Expression result = ParserExpression(error, epsilon, position + 1);
            return new Cos(result);
        }
        if (code[position].name == TokenName.tan)
        {
            Expression result = ParserExpression(error, epsilon, position + 1);
            return new Tan(result);
        }
        if (code[position].name == TokenName.log)
        {
            if (code[position + 1].name == TokenName.openP)
            {
                List<Expression> result = ParserH(error, new(), position + 2);
                return new Log(result[1]);
            }
            error.Add(new Errors(ErrorCode.Sintaxis, "("));
        }
        if (code[position].name == TokenName.squart)
        {
            Expression result = ParserExpression(error, epsilon, position + 1);
            return new Squart(result);
        }
        if (code[position].name == TokenName.chain)                                          //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            return new Chain_Lietarls(code[position].value);
        }
        if (code[position].name == TokenName.number)
        {
            return new Number(double.Parse(code[position].value));
        }
        if (code[position].name == TokenName.id)
        {
            foreach (var item in Context.funcs)
            {
                if (item.Name == code[position].value)
                {
                    if (code[position + 1].name == TokenName.openP)
                    {
                        Token f = code[position];

                        List<Expression> result = ParserH(error, new(), position + 2);
                        Expression func = new FuncCall(result, item);

                        if (!Context.IsDeclare_Func)
                        {
                            IsFunc = true;
                        }
                        return func;
                    }
                    break;
                }
            }
            return new ID(code[position], null!);
        }
        if (code[position].name == TokenName.PI)
        {
            return new Number(Math.PI);
        }
        if (code[position].name == TokenName.True)
        {
            return new Bool(true);
        }
        if (code[position].name == TokenName.False)
        {
            return new Bool(false);
        }
        if (code[position].name == TokenName.openP)
        {
            Expression result = ParserCode(error, position + 1);

            if (code[position].name == TokenName.closeP)
                return result;

            error.Add(new Errors(ErrorCode.Sintaxis, ""));
        }

        return null!;
    }

}