namespace Hulk
{
    public enum TokenName
    {
        number, text, True, False, not,
        add, star, sub, div, pow, squart,
        openP, closeP, openL, closeL, comilla,
        equal, mod, menor, mayor, exc, MenorEqual, MayorEqual, EqualEqual, distint, or, and, implic, concat,chain,
        id, In, let, If, Else, them, print, PI, function, log, var, end, pointEqualToken, coma, sen, cos, tan,
        EOF
    }
    public enum TokenType
    {
        Symbol, Keyword, Number, Text, Id,
    }
    public class Token
    {
        public TokenName name;
        public string value;

        public TokenType type;

        // int potition;

        public Token(TokenName name, string value, TokenType type)
        {
            this.name = name;
            this.value = value;
            this.type = type;
        }

        public string GetValue()
        {
            return value;
        }

    }

    public class AddToken : Token
    {
        public AddToken() : base(TokenName.add, "+", TokenType.Symbol) { }
    }
    public class StarToken : Token
    {
        public StarToken() : base(TokenName.star, "*", TokenType.Symbol) { }
    }
    public class SubToken : Token
    {
        public SubToken() : base(TokenName.sub, "-", TokenType.Symbol) { }
    }
    public class DivToken : Token
    {
        public DivToken() : base(TokenName.div, "/", TokenType.Symbol) { }
    }
    public class ModToken : Token
    {
        public ModToken() : base(TokenName.mod, "%", TokenType.Symbol) { }
    }
    public class PowToken : Token
    {
        public PowToken() : base(TokenName.pow, "^", TokenType.Symbol) { }
    }
    public class ComillaToken : Token
    {
        public ComillaToken() : base(TokenName.comilla, "\"", TokenType.Symbol) { }
    }
    public class OpenLToken : Token
    {
        public OpenLToken() : base(TokenName.openL, "{", TokenType.Symbol) { }
    }
    public class CloseLToken : Token
    {
        public CloseLToken() : base(TokenName.closeL, "}", TokenType.Symbol) { }
    }
    public class OpenPToken : Token
    {
        public OpenPToken() : base(TokenName.openP, "(", TokenType.Symbol) { }
    }
    public class ClosePToken : Token
    {
        public ClosePToken() : base(TokenName.closeP, ")", TokenType.Symbol) { }
    }
    public class ComaToken : Token
    {
        public ComaToken() : base(TokenName.coma, ",", TokenType.Symbol) { }
    }
    public class EqualToken : Token
    {
        public EqualToken() : base(TokenName.equal, "=", TokenType.Symbol) { }
    }
    public class PointEqualToken : Token
    {
        public PointEqualToken() : base(TokenName.equal, ":=", TokenType.Symbol) { }
    }
    public class MenorToken : Token
    {
        public MenorToken() : base(TokenName.menor, "<", TokenType.Symbol) { }
    }
    public class MayorToken : Token
    {
        public MayorToken() : base(TokenName.mayor, ">", TokenType.Symbol) { }
    }
    public class ExcToken : Token
    {
        public ExcToken() : base(TokenName.exc, "!", TokenType.Symbol) { }
    }
    public class MenorEqualToken : Token
    {
        public MenorEqualToken() : base(TokenName.MenorEqual, "<=", TokenType.Symbol) { }
    }
    public class MayorEqualToken : Token
    {
        public MayorEqualToken() : base(TokenName.MayorEqual, ">=", TokenType.Symbol) { }
    }
    public class EqualEqualToken : Token
    {
        public EqualEqualToken() : base(TokenName.EqualEqual, "==", TokenType.Symbol) { }
    }
    public class DistintToken : Token
    {
        public DistintToken() : base(TokenName.distint, "!=", TokenType.Symbol) { }
    }
    public class OrToken : Token
    {
        public OrToken() : base(TokenName.or, "|", TokenType.Symbol) { }
    }
    public class AndToken : Token
    {
        public AndToken() : base(TokenName.and, "&", TokenType.Symbol) { }
    }
    public class ImplicToken : Token
    {
        public ImplicToken() : base(TokenName.implic, "=>", TokenType.Symbol) { }
    }

    public class ConcatToken : Token
    {
        public ConcatToken() : base(TokenName.concat, "@", TokenType.Symbol) { }
    }
    public class NumberToken : Token
    {
        public NumberToken(TokenName type, string value) : base(type, value, TokenType.Number) { }
    }
    public class TextToken : Token
    {
        public TextToken(TokenName type, string value) : base(type, value, TokenType.Text) { }
    }
    public class TrueToken : Token
    {
        public TrueToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class FalseToken : Token
    {
        public FalseToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
     public class NotToken : Token
    {
        public NotToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class SenToken : Token
    {
        public SenToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class CosToken : Token
    {
        public CosToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class TanToken : Token
    {
        public TanToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class IdToken : Token
    {
        public IdToken(TokenName type, string value) : base(type, value, TokenType.Id) { }
    }
    public class LetToken : Token
    {
        public LetToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class InToken : Token
    {
        public InToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class IfToken : Token
    {
        public IfToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class ElseToken : Token
    {
        public ElseToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class ThemToken : Token
    {
        public ThemToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class PiToken : Token
    {
        public PiToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class SquartToken : Token
    {
        public SquartToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class LogToken : Token
    {
        public LogToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class FunctionToken : Token                                                               ////////////
    {
        public FunctionToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class PrintToken : Token
    {
        public PrintToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class VarToken : Token                                                                     ////////////
    {
        public VarToken(TokenName type, string value) : base(type, value, TokenType.Keyword) { }
    }
    public class EndToken : Token
    {
        public EndToken() : base(TokenName.end, ";", TokenType.Symbol) { }
    }

    public class EOFToken : Token
    {
        public EOFToken() : base(TokenName.EOF, "#", TokenType.Symbol) { }
    }
}
