namespace Hulk;

public class Parser
{
    public Parser(Code Code)
    {
        this.Code = Code;
    }

    //Lista de tokens 
    public Code Code { get; set; }
    public bool IsFunc;

    //Las lineas de codigo solo deben empesar con las palabras print, if, let,function o un numero
    //Lo primero que hace es preguntar cual de esas palabras es la primera y automaticamente llama al parser q corresponda

    public Expression ParserCode(List<Errors> error, Expression epsilon = null!)
    {
        if (!Code.CanLookAhead()) return epsilon;

        if (Code.LookAhead().name == TokenName.print)
        {
            Code.MoveNext(1);
            Expression result = ParserCode(error, epsilon);
            Expression print = new Print(result);

            return print;
        }

        if (Code.LookAhead().name == TokenName.let)
        {
            if (Code.LookAhead(1).name == TokenName.id)
            {
                Token id = Code.LookAhead();
                if (Code.LookAhead(2).name == TokenName.equal)
                {
                    Code.MoveNext(1);
                    Expression result = ParserCode(error, epsilon);
                    Expression assin = new AssingExpression(id, result);
                    epsilon = assin;
                    return ParserB(error, epsilon);
                }

                error.Add(new Errors(ErrorCode.Sintaxis, Code.LookAhead().value));
            }

            error.Add(new Errors(ErrorCode.Sintaxis, Code.LookAhead().value));
        }

        if (Code.LookAhead().name == TokenName.function)
        {
            if (Code.LookAhead().name == TokenName.id)
            {
                Token t = Code.LookAhead();
                if (Code.LookAhead().name == TokenName.openP)
                {
                    List<Token> result = ParserID(error, new());
                    if (Code.LookAhead().name == TokenName.id)
                    {
                        Function f = new Function(t.value, result, null!);

                        Expression instrutcions = ParserCode(error);
                        f.Instructions = instrutcions;

                        return f;
                    }
                    error.Add(new Errors(ErrorCode.Sintaxis, Code.LookAhead().value));
                }
                error.Add(new Errors(ErrorCode.Sintaxis, Code.LookAhead().value));
            }
            error.Add(new Errors(ErrorCode.Sintaxis, Code.LookAhead().value));
        }

        if (Code.LookAhead().name == TokenName.If)
        {
            Code.MoveNext(1);
            Expression If = ParserCode(error, epsilon);
            Expression Them = ParserCode(error, If);

            if (Code.LookAhead().name == TokenName.Else)
            {
                Code.MoveNext(1);
                Expression Else = ParserCode(error, Them);
                Expression ITE = new If_them_else(If, Them, Else);

                return ITE;
            }

            error.Add(new Errors(ErrorCode.Sintaxis, Code.LookAhead().value));
        }

        return ParserD(error, epsilon);
    }
    //B --> in A | e
    public Expression ParserB(List<Errors> error, Expression epsilon)
    {
        if (Code.Next(TokenName.In)) { }
        else
        {
            error.Add(new Errors(ErrorCode.Sintaxis, Code.LookAhead().value));
            return epsilon;
        }
        Expression result = ParserCode(error, epsilon);
        Expression Let_in = new LetInExpression(epsilon, result);
        return Let_in;
    }
    //ID --> id | id | B| e
    public List<Token> ParserID(List<Errors> error, List<Token> ID)
    {
        if (Code.Next(TokenName.id))
        {
            ID.Add(Code.LookAhead());
            Code.Next();
            if (Code.Next(TokenName.id))
            {

            }
            if (Code.Next(TokenName.closeP))
                return ID;
        }

        error.Add(new Errors(ErrorCode.Sintaxis, Code.LookAhead().value));
        return null!;
    }
    //H --> E | E,H | e
    public List<Expression> ParserH(List<Errors> error, List<Expression> exp)
    {
        for (int i = Code.Position; i < Code.tokens.Count; i++) //i comienza en el token en el que estoy
        {
            var expression = ParserCode(error);
            exp.Add(expression);
            if (Code.Next(TokenName.openP))
            {
                return exp;
            }
        }

        error.Add(new Errors(ErrorCode.Sintaxis, Code.LookAhead().value));
        return null!;
    }
    //D --> I And_Or
    public Expression ParserD(List<Errors> error, Expression epsilon)
    {
        Expression result;
        if (Code.Next(TokenName.not))
        {
            Expression I = ParserI(error, epsilon);
            Expression Not = new NotExpresion(I);
            result = ParserAnd_Or(error, Not);
        }
        else
        {
            Expression I = ParserI(error, epsilon);
            result = ParserAnd_Or(error, I);
        }
        return result;
    }
    //And_Or --> and A | or A | e
    public Expression ParserAnd_Or(List<Errors> error, Expression epsilon)
    {
        Expression result;

        if (Code.Next(TokenName.and))
        {
            result = ParserD(error, epsilon);
            Expression Add = new AndExpression(epsilon, result);
            return Add;
        }
        if (Code.Next(TokenName.or))
        {
            result = ParserD(error, epsilon);
            Expression Sub = new OrExpression(epsilon, result);
            return Sub;
        }

        return epsilon;
    }
    //I --> EC 
    public Expression ParserI(List<Errors> error, Expression epsilon)
    {
        Expression E = ParserFactor(error, epsilon);
        Expression C = ParserY(error, E);

        return C;
    }
    //C --> >E | <E | >=E | <=E | ==E | !E | e
    public Expression ParserComparer(List<Errors> error, Expression epsilon)
    {
        if (Code.Next(TokenName.mayor))
        {
            Expression result = ParserExpression(error, epsilon);
            Expression mayor = new MayorExpression(epsilon, result);
            return mayor;
        }
        if (Code.Next(TokenName.menor))
        {
            Expression result = ParserExpression(error, epsilon);
            Expression menor = new MenorExpression(epsilon, result);
            return menor;
        }
        if (Code.Next(TokenName.MayorEqual))
        {
            Expression result = ParserExpression(error, epsilon);
            Expression MayorEqual = new MayorEqualExpression(epsilon, result);
            return MayorEqual;
        }
        if (Code.Next(TokenName.MenorEqual))
        {
            Expression result = ParserExpression(error, epsilon);
            Expression MenorEqual = new MenorEqualExpression(epsilon, result);
            return MenorEqual;
        }
        if (Code.Next(TokenName.distint))
        {
            Expression result = ParserExpression(error, epsilon);
            Expression distint = new DistintExpression(epsilon, result);
            return distint;
        }
        if (Code.Next(TokenName.EqualEqual))
        {
            Expression result = ParserExpression(error, epsilon);
            Expression EqualEqual = new EqualExpression(epsilon, result);
            return EqualEqual;
        }

        return epsilon;
    }
    //E --> TX
    public Expression ParserExpression(List<Errors> error, Expression epsilon)
    {
        Expression T = ParserTerm(error, epsilon);
        Expression X = ParserX(error, T);

        return X;
    }
    //X --> +E | -E | e
    public Expression ParserX(List<Errors> error, Expression epsilon)
    {
        Expression result;

        if (Code.Next(TokenName.add))
        {
            result = ParserExpression(error, epsilon);
            Expression Add = new Add(epsilon, result);
            return Add;
        }
        if (Code.Next(TokenName.sub))
        {
            result = ParserExpression(error, epsilon);
            Expression Sub = new Sub(epsilon, result);
            return Sub;
        }

        return epsilon;
    }
    //T --> FY
    public Expression ParserTerm(List<Errors> error, Expression epsilon)
    {
        Expression F = ParserFactor(error, epsilon);
        Expression Y = ParserY(error, F);

        return Y;
    }
    //Y --> *E | /E | %E | ^E | e
    public Expression ParserY(List<Errors> error, Expression epsilon)
    {
        Expression result;

        if (Code.Next(TokenName.star))
        {
            result = ParserExpression(error, epsilon);
            Expression Mult = new Start(epsilon, result);
            return Mult;
        }
        if (Code.Next(TokenName.div))
        {
            result = ParserExpression(error, epsilon);
            Expression Div = new Div(epsilon, result);
            return Div;
        }
        if (Code.Next(TokenName.mod))
        {
            result = ParserExpression(error, epsilon);
            Expression Mod = new Mod(epsilon, result);
            return Mod;
        }
        if (Code.Next(TokenName.pow))
        {
            result = ParserExpression(error, epsilon);
            Expression Pow = new Pow(epsilon, result);
            return Pow;
        }
        return epsilon;
    }
    //F --> int | (A) | true | false | id | Func(H) | @-Concat | 
    public Expression ParserFactor(List<Errors> error, Expression epsilon)
    {

        if (Code.Next(TokenName.sen))
        {
            Expression result = ParserExpression(error, epsilon);
            return new Sen(result);
        }
        if (Code.Next(TokenName.cos))
        {
            Expression result = ParserExpression(error, epsilon);
            return new Cos(result);
        }
        if (Code.Next(TokenName.tan))
        {
            Expression result = ParserExpression(error, epsilon);
            return new Tan(result);
        }
        if (Code.Next(TokenName.log))
        {
            if (Code.LookAhead().name == TokenName.openP)
            {
                List<Expression> result = ParserH(error, new());              
                return new Log(result[1]);
            }
            error.Add(new Errors(ErrorCode.Sintaxis, "("));
        }
        if (Code.Next(TokenName.squart))
        {
            Expression result = ParserExpression(error, epsilon);
            return new Squart(result);
        }
        if (Code.Next(TokenName.concat))
        {
            return new Concat(Code.LookAhead().value);
        }
        if (Code.Next(TokenName.number))
        {
            return new Number(double.Parse(Code.LookAhead().value));
        }
        if (Code.Next(TokenName.id))
        {
            foreach (var item in Context1.funcs)
            {
                if (item.Name == Code!.LookAhead().value)
                {
                    if (Code.Next(TokenName.openP))
                    {
                        Token f = Code.LookAhead();

                        List<Expression> result = ParserH(error, new());                                    
                        Expression func = new FuncCall(result, item);

                        if (!Context1.IsDeclare_Func)                                              //!
                        {
                             IsFunc = true;
                        }
                        return func;
                    }
                    break;
                }
            }
            return new ID(Code.LookAhead(), null!);
        }
        if (Code.Next(TokenName.openP))
        {
            return new Number(Math.PI);
        }
        if (Code.Next(TokenName.True))
        {
            return new Bool(true);
        }
        if (Code.Next(TokenName.False))
        {
            return new Bool(false);
        }
        if (Code.Next(TokenName.openP))
        {
            Expression result = ParserCode(error);

            if (Code.Next(TokenName.closeP))
                return result;

            error.Add(new Errors(ErrorCode.Sintaxis, ""));
        }

        return null!;
    }

}