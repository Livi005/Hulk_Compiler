namespace Hulk;

public static class Context
{
    public static List<Function> funcs = new List<Function>();
    public static Scope General = new Scope(null!, new(), new());
    public static bool IsDeclare_Func;
}