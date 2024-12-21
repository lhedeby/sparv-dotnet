public class StringNode : IAstNode
{
    private string _value;
    public StringNode(Token token)
    {
        _value = token.Value[1..(token.Value.Length - 1)];
    }

    public AnalyzerKind Analyze(Analyzer a) => AnalyzerKind.String;

    public object? Interpret(Interpreter inter)
    {
        return _value;
    }

    public override string? ToString()
    {
        return $"(string({_value}))";
    }
}


