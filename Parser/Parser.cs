namespace Hulk;

public class Parser
{
    public Parser(List<Token> code)
    {
        this.code = code;
        donde_me_quede = 0;
    }

    //Lista de tokens 
    public int donde_me_quede { get; set; }
    public List<Token> code { get; }

    public bool IsFunc;

    //Las lineas de codigo solo deben empesar con las palabras print, if, let,function o un numero
    //Lo primero que hace es preguntar cual de esas palabras es la primera y automaticamente llama al parser q corresponda

    public Expression ParserCode(List<Errors> error, int position, Expression epsilon = null!)
    {
        if (code[position].name == TokenName.EOF)
            return epsilon;

        if (code[position].name == TokenName.print)
        {
            donde_me_quede += 1;
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
                    donde_me_quede += 3;
                    Expression result = ParserCode(error, position + 3, epsilon);
                    position = donde_me_quede;
                    Expression asig = new AssingExpression(id, result);
                    epsilon = asig;

                    return ParserB(error, epsilon, position);
                }

                error.Add(new Errors(ErrorCode.Sintaxis, "detras del nombre de una variable debe ir un ="));
            }

            error.Add(new Errors(ErrorCode.Sintaxis, "debe declararse una variable poniendose el id"));
        }

        if (code[position].name == TokenName.function && donde_me_quede == 0)
        {
            if (code[position + 1].name == TokenName.id)
            {
                Token func_name = code[position + 1];
                if (code[position + 2].name == TokenName.openP)
                {
                    donde_me_quede += 3;
                    List<Token> param = ParserID(error, new(), position + 3);
                    position = donde_me_quede;
                    if (code[position].name == TokenName.implic)                                         //!.................................
                    {
                        Context.IsDeclare_Func = true;
                        Function func = new Function(func_name.value, param, null!);
                        Context.funcs.Add(func);

                        donde_me_quede += 1;
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
            donde_me_quede += 1;
            Expression If = ParserCode(error, position + 1, epsilon);
            Expression Them = ParserCode(error, donde_me_quede, If);

            if (code[donde_me_quede].name == TokenName.Else)
            {
                donde_me_quede += 1;
                Expression Else = ParserCode(error, donde_me_quede, Them);
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
        {
            donde_me_quede = position + 1;
            Expression result = ParserCode(error, position + 1, epsilon);
            Expression Let_in = new LetInExpression(epsilon, result);

            return Let_in;
        }
        else
        {
            error.Add(new Errors(ErrorCode.Sintaxis, "le falto el in"));
            donde_me_quede = position + 1;
            return epsilon;
        }
    }
    //ID --> id | B| e
    public List<Token> ParserID(List<Errors> error, List<Token> ID, int position)
    {
        if (code[position].name == TokenName.id)
        {
            ID.Add(code[position]);
            donde_me_quede = position + 1;
            return ParserID(error, ID, position + 1);
        }
        if (code[donde_me_quede].name == TokenName.closeP)
        {
            donde_me_quede += 1;
            return ID;
        }

        error.Add(new Errors(ErrorCode.Sintaxis, code[position].value));

        donde_me_quede = 0;
        return null!;
    }
    //H --> E | H | e
    public List<Expression> ParserH(List<Errors> error, List<Expression> exp, int position)
    {
        for (int i = position; i < code.Count; i++)                 //i comienza en el token en el que estoy
        {
            var expression = ParserCode(error, i);
            exp.Add(expression);
            i = donde_me_quede;
            if (code[donde_me_quede].name == TokenName.closeP)
            {
                donde_me_quede += 1;
                return exp;
            }
        }

        error.Add(new Errors(ErrorCode.Sintaxis, code[position].value));
        donde_me_quede += 1;
        return null!;
    }
    //D --> I And_Or
    public Expression ParserD(List<Errors> error, int position, Expression epsilon = null!)
    {
        Expression result;
        if (code[position].name == TokenName.not)
        {
            donde_me_quede = position + 1;
            Expression exp = ParserI(error, epsilon, position + 1);
            Expression Not = new NotExpresion(exp);
            result = ParserAnd_Or(error, Not, donde_me_quede);
        }
        else
        {
            Expression exp = ParserI(error, epsilon, position);
            result = ParserAnd_Or(error, exp, donde_me_quede);
        }

        return result;
    }
    //And_Or --> and A | or A | e
    public Expression ParserAnd_Or(List<Errors> error, Expression epsilon, int position)
    {
        Expression result;

        if (code[position].name == TokenName.and)
        {
            donde_me_quede = position + 1;
            result = ParserD(error, position + 1, epsilon);
            Expression Add = new AndExpression(epsilon, result);
            return Add;
        }
        if (code[position].name == TokenName.or)
        {
            donde_me_quede = position + 1;
            result = ParserD(error, position + 1, epsilon);
            Expression Sub = new OrExpression(epsilon, result);
            return Sub;
        }

        return epsilon;
    }
    //I --> EC 
    public Expression ParserI(List<Errors> error, Expression epsilon, int position)
    {
        Expression E = ParserExpression(error, epsilon, position);
        Expression C = ParserComparer(error, E, donde_me_quede);

        return C;
    }
    //C --> >E | <E | >=E | <=E | ==E | !E | e
    public Expression ParserComparer(List<Errors> error, Expression epsilon, int position)
    {
        if (code[position].name == TokenName.mayor)
        {
            donde_me_quede = position + 1;
            Expression result = ParserExpression(error, epsilon, position + 1);
            Expression mayor = new MayorExpression(epsilon, result);
            return mayor;
        }
        if (code[position].name == TokenName.menor)
        {
            donde_me_quede = position + 1;
            Expression result = ParserExpression(error, epsilon, position + 1);
            Expression menor = new MenorExpression(epsilon, result);
            return menor;
        }
        if (code[position].name == TokenName.MayorEqual)
        {
            donde_me_quede = position + 1;
            Expression result = ParserExpression(error, epsilon, position + 1);
            Expression MayorEqual = new MayorEqualExpression(epsilon, result);
            return MayorEqual;
        }
        if (code[position].name == TokenName.MenorEqual)
        {
            donde_me_quede = position + 1;
            Expression result = ParserExpression(error, epsilon, position + 1);
            Expression MenorEqual = new MenorEqualExpression(epsilon, result);
            return MenorEqual;
        }
        if (code[position].name == TokenName.distint)
        {
            donde_me_quede = position + 1;
            Expression result = ParserExpression(error, epsilon, position + 1);
            Expression distint = new DistintExpression(epsilon, result);
            return distint;
        }
        if (code[position].name == TokenName.EqualEqual)
        {
            donde_me_quede = position + 1;
            Expression result = ParserExpression(error, epsilon, position + 1);
            Expression EqualEqual = new EqualExpression(epsilon, result);
            return EqualEqual;
        }

        return epsilon;
    }
    //E --> TX
    public Expression ParserExpression(List<Errors> error, Expression epsilon, int position)
    {
        Expression T = ParserTerm(error, epsilon, position);
        Expression X = ParserX(error, T, donde_me_quede);

        return X;
    }
    //X --> +E | -E | e
    public Expression ParserX(List<Errors> error, Expression epsilon, int position)
    {
        Expression result;

        if (code[position].name == TokenName.add)
        {
            donde_me_quede = position + 1;
            result = ParserExpression(error, epsilon, position + 1);
            Expression Add = new Add(epsilon, result);
            return Add;
        }
        if (code[position].name == TokenName.sub)
        {
            donde_me_quede = position + 1;
            result = ParserExpression(error, epsilon, position + 1);
            Expression Sub = new Sub(epsilon, result);
            return Sub;
        }
        if (code[position].name == TokenName.concat)
        {
            donde_me_quede = position + 1;
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
        Expression Y = ParserY(error, F, donde_me_quede);

        return Y;
    }
    //Y --> *E | /E | %E | ^E | e
    public Expression ParserY(List<Errors> error, Expression epsilon, int position)
    {
        Expression result;

        if (code[position].name == TokenName.star)
        {
            donde_me_quede = position + 1;
            result = ParserExpression(error, epsilon, position + 1);
            Expression Mult = new Start(epsilon, result);
            return Mult;
        }
        if (code[position].name == TokenName.div)
        {
            donde_me_quede = position + 1;
            result = ParserExpression(error, epsilon, position + 1);
            Expression Div = new Div(epsilon, result);
            return Div;
        }
        if (code[position].name == TokenName.mod)
        {
            donde_me_quede = position + 1;
            result = ParserExpression(error, epsilon, position + 1);
            Expression Mod = new Mod(epsilon, result);
            return Mod;
        }
        if (code[position].name == TokenName.pow)
        {
            donde_me_quede = position + 1;
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
            donde_me_quede = position + 1;
            Expression result = ParserExpression(error, epsilon, position + 1);
            return new Sen(result);
        }
        if (code[position].name == TokenName.cos)
        {
            donde_me_quede = position + 1;
            Expression result = ParserExpression(error, epsilon, position + 1);
            return new Cos(result);
        }
        if (code[position].name == TokenName.tan)
        {
            donde_me_quede = position + 1;
            Expression result = ParserExpression(error, epsilon, position + 1);
            return new Tan(result);
        }
        if (code[position].name == TokenName.log)
        {
            if (code[position + 1].name == TokenName.openP)
            {
                donde_me_quede = position + 2;
                List<Expression> result = ParserH(error, new(), position + 2);
                return new Log(result[1]);
            }
            error.Add(new Errors(ErrorCode.Sintaxis, "("));
        }
        if (code[position].name == TokenName.squart)
        {
            donde_me_quede = position + 1;
            Expression result = ParserExpression(error, epsilon, position + 1);
            return new Squart(result);
        }
        // if (code[position].name == TokenName.chain)               //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // {
        //     donde_me_quede = position + 1;
        //     return new Chain_Lietarls(code[position].value);
        // }
        if (code[position].name == TokenName.number)
        {
            donde_me_quede = position + 1;
            return new Number(double.Parse(code[position].value));
        }
        if (code[position].name == TokenName.id)
        {
            donde_me_quede = position + 1;
            foreach (var item in Context.funcs)
            {
                if (item.Name == code[position].value)
                {
                    if (code[position + 1].name == TokenName.openP)
                    {
                        Token f = code[position];
                        donde_me_quede = position + 2;
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
            donde_me_quede = position + 1;
            return new Number(Math.PI);
        }
        if (code[position].name == TokenName.True)
        {
            donde_me_quede = position + 1;
            return new Bool(true);
        }
        if (code[position].name == TokenName.False)
        {
            donde_me_quede = position + 1;
            return new Bool(false);
        }
        if (code[position].name == TokenName.openP)
        {
            donde_me_quede = position + 1;
            Expression result = ParserCode(error, position + 1);

            if (code[donde_me_quede].name == TokenName.closeP)
            {
                donde_me_quede += 1;
                return result;
            }

            error.Add(new Errors(ErrorCode.Sintaxis, ""));
        }

        return null!;
    }
}