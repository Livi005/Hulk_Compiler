namespace Hulk;

public static class Program
{
    public static void Main()
    {
        System.Console.WriteLine("Escriba:");
        string text = Console.ReadLine()!;


        List<Errors> errores = new List<Errors>();
        Tokenizar tokenizar = new Tokenizar(text, errores);
        List<Token> tokens = tokenizar.Analizar();

        foreach (Token token in tokens)
        {
            Console.WriteLine(token.type + " ---- " + token.value);
        }

        Parser parser = new Parser(tokens);
        Expression program = parser.ParserCode(errores, 0);

        if (errores.Count > 0)
        {
            foreach (Errors error in errores)
            {
                Console.WriteLine("{0}, {1}", error.Code, error.Argument);
            }
        }
        else
        {
            Scope scope = new Scope(Context.General.Parent!, Context.General.Value, Context.General.Type);

            program.CheckSemantic(errores, scope);

            if (errores.Count > 0)
            {
                foreach (Errors error in errores)
                {
                    Console.WriteLine("{0}, {1}", error.Code, error.Argument);
                }
            }
            else
            {
                program.Evaluate();

                Console.WriteLine(program);
            }
        }
    }
}
