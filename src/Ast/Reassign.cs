public class Reassign(IAstNode lhs, IAstNode rhs) : IAstNode
{
    public AnalyzerKind Analyze(Analyzer a)
    {
        if (lhs is Variable s)
        {
            if (!a.VarExists(s.Name))
            {
                a.AddError(new SparvException($"No variable named '{s.Name}' exists in this scope", s.Token.Line, s.Token.Start, s.Token.End));
            }

            // typechecking?
            // if (lhs.Analyze(a) != rhs.Analyze(a))
            // {
            //     a.AddError(new SparvException($"Trying to change type of var {s.Name}", s.Token.Line, s.Token.Start, s.Token.End));
            // }
        }

        return AnalyzerKind.Nil;
    }

    public object? Interpret(Interpreter inter)
    {
        if (lhs is Variable)
        {
            var name = ((Variable)lhs).Name;
            inter.Reassign(name, rhs.Interpret(inter));
            return null;
        }
        if (lhs is Index index)
        {
            var l = index.List.Interpret(inter) as RuntimeList;
            var indexer = index.Indexer.Interpret(inter) switch
            {
                int i => i,
                double d => (int)d,
                _ => throw new Exception("sad")
            };

            var res = rhs.Interpret(inter);
            l.list[indexer] = res;
            return null;
        }
        throw new Exception("TODO: Cant set lhs");
    }
}


