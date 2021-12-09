using System.Collections.Generic;

namespace Panosen.Compling.LL1
{
    public class GrammarNode
    {
        public GrammarNode(Symbol data)
        {
            Data = data;
        }

        public Symbol Data { get; set; }

        public List<GrammarNode> Children { get; set; }
    }

    public static class TINYNodeExtension
    {
        public static void AddChild(this GrammarNode grammarNode, GrammarNode child)
        {
            if (grammarNode.Children == null)
            {
                grammarNode.Children = new List<GrammarNode>();
            }

            grammarNode.Children.Add(child);
        }

        public static void AddChild(this GrammarNode grammarNode, int index, GrammarNode child)
        {
            if (grammarNode.Children == null)
            {
                grammarNode.Children = new List<GrammarNode>();
            }

            grammarNode.Children.Insert(index, child);
        }

        public static bool IsNotEmpty(this GrammarNode grammarNode)
        {
            switch (grammarNode.Data.Type)
            {
                case SymbolType.NonTerminal:
                    {
                        if (grammarNode.Children == null || grammarNode.Children.Count == 0)
                        {
                            return false;
                        }

                        foreach (var child in grammarNode.Children)
                        {
                            if (IsNotEmpty(child))
                            {
                                return true;
                            }
                        }

                        return false;
                    }

                case SymbolType.Terminal:
                    {
                        return true;
                    }

                case SymbolType.Epsilon:
                    {
                        return false;
                    }

                case SymbolType.None:
                default:
                    {
                        return true;
                    }
            }
        }
    }

}
