namespace Hulk;

public static class Program
{
    public static void Main()
    {
        System.Console.WriteLine("Escriba:");
        string text = Console.ReadLine()!;
        if (text == "") return;

        List<Errors> errores = new List<Errors>();
        Tokenizar tokenizar = new Tokenizar(text, errores);
        List<Token> tokens = tokenizar.Analizar();

        foreach (Token token in tokens)
        {
            Console.WriteLine(token.name + " ---- " + token.value);
        }

        Parser parser = new Parser(tokens);
        Expression program = parser.ParserCode(errores, 0);

        // Scope scope = new Scope(Context.General.Parent!, Context.General.Value, Context.General.Type);
        program.Scope(Context.General);

        if (!Context.IsDeclare_Func)
            if (errores.Count > 0)
            {
                foreach (Errors error in errores)
                {
                    Console.WriteLine("{0}, {1}", error.Code, error.Argument);
                }
            }
            else
            {
                program.CheckSemantic(errores);

                if (errores.Count > 0)
                {
                    foreach (Errors error in errores)
                    {
                        Console.WriteLine("{0}, {1}", error.Code, error.Argument);
                    }
                }
                else
                {
                    Console.WriteLine(program.Evaluate());
                }
            }

        Context.IsDeclare_Func = false;
        Context.General = new(null!, new(), new());
        //volver a escribir;
        Main();
    }
}
