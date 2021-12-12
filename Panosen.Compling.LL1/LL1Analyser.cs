using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.LL1
{

    public class LL1Analyser
    {
        /// <summary>
        /// 分析输入串
        /// 若输入串能构成正确语法树，返回True，否则返回False
        /// </summary>
        /// <param name="tokenList">输入串</param>
        /// <param name="Root">语法树根节点</param>
        /// <returns>若输入串能构成正确语法树，返回True，否则返回False</returns>
        public static bool Analyse(TokenCollection tokenList, out GrammarNode Root, out Token ErrorToken, Grammar grammar)
        {
            var analysisTable = grammar.MakeAnalysisTable();

            Stack<Symbol> symbolStack = new Stack<Symbol>();
            Stack<GrammarNode> NodeStack = new Stack<GrammarNode>();

            var start = grammar.Rules[0].Left;

            //assume
            symbolStack.Push(start);
            Root = new GrammarNode(start);
            ErrorToken = default(Token);
            NodeStack.Push(Root);

            int index = 0;

            while (symbolStack.Count != 0 || index < tokenList.Count())
            {
                if (symbolStack.Count == 0)
                {
                    return false;
                }

                //栈顶元素
                var symbolStackTop = symbolStack.Peek();
                //栈顶节点
                var nodeStackTop = NodeStack.Peek();
                //当前输入
                Token inputToken = index < tokenList.Count() ? tokenList[index] : null;

                //当前元素等于栈顶元素
                if (inputToken != null && symbolStackTop.Value.Equals(inputToken.Value))
                {
                    symbolStack.Pop();
                    NodeStack.Pop();
                    index++;
                }

                //当前元素不等于栈顶元素X，但是栈顶元素X是非终结符
                else if (symbolStackTop.Type == SymbolType.NonTerminal)
                {
                    var ll1Item = analysisTable.FirstOrDefault(v => v.Row.Equals(symbolStackTop) && inputToken != null && v.Col.Value.Equals(inputToken.Value));

                    //没有找到[symbolStackTop,inputSymbol]
                    if (ll1Item == null)
                    {
                        //当前输入是结束符，匹配成功
                        if (inputToken == null)
                        {
                            return true;
                        }

                        //X可以归结为ε
                        else if (analysisTable.Any(v => v.Row.Equals(symbolStackTop) && v.Value.Right.Contains(Symbols.Epsilon)))
                        {
                            nodeStackTop.AddChild(new GrammarNode(Symbols.Epsilon));
                            symbolStack.Pop();
                            NodeStack.Pop();
                        }
                        else
                        {
                            ErrorToken = inputToken;
                            return false;
                        }
                    }

                    //L!=null
                    else
                    {
                        symbolStack.Pop();
                        NodeStack.Pop();

                        // A -> ε
                        if (ll1Item.Value.Right.Contains(Symbols.Epsilon))
                        {
                            nodeStackTop.AddChild(new GrammarNode(Symbols.Epsilon));
                            continue;
                        }
                        for (int i = ll1Item.Value.Right.Count - 1; i >= 0; i--)
                        {
                            symbolStack.Push(ll1Item.Value.Right[i]);

                            GrammarNode newNode = new GrammarNode(ll1Item.Value.Right[i]);
                            NodeStack.Push(newNode);
                            nodeStackTop.AddChild(0, newNode);
                        }
                    }
                }
                else
                {
                    ErrorToken = inputToken;
                    return false;
                }
            }
            return true;
        }
    }
}
