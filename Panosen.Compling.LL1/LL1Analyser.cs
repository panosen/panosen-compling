using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.LL1
{
    public class TINYNode
    {
        public TINYNode()
        {
            Children = new List<TINYNode>();
        }
        public TINYNode(string data)
        {
            NonTerminalData = data;
            Children = new List<TINYNode>();
        }
        public string NonTerminalData;
        public List<TINYNode> Children;
    }

    public class LL1Analyser
    {
        /// <summary>
        /// 分析输入串
        /// 若输入串能构成正确语法树，返回True，否则返回False
        /// </summary>
        /// <param name="tokenList">输入串</param>
        /// <param name="Root">语法树根节点</param>
        /// <returns>若输入串能构成正确语法树，返回True，否则返回False</returns>
        public static bool Analyse(List<Symbol> tokenList, out TINYNode Root, out Symbol ErrorToken, Grammar grammar)
        {
            var analysisTable = grammar.MakeAnalysisTable();

            Stack<Symbol> symbolStack = new Stack<Symbol>();
            Stack<TINYNode> NodeStack = new Stack<TINYNode>();

            var start = grammar.Rules[0].Left;

            //assume
            symbolStack.Push(start);
            Root = new TINYNode(start.Value);
            ErrorToken = default(Symbol);
            NodeStack.Push(Root);

            int index = 0;


            while (symbolStack.Count != 0)
            {
                //栈顶元素
                var symbolStackTop = symbolStack.Peek();
                //栈顶节点
                var nodeStackTop = NodeStack.Peek();
                //当前输入
                Symbol inputSymbol = index < tokenList.Count ? tokenList[index] : Symbols.Dollar;

                //当前元素等于栈顶元素
                if (symbolStackTop.Equals(inputSymbol))
                {
                    symbolStack.Pop();
                    NodeStack.Pop();
                    index++;
                }

                //当前元素不等于栈顶元素X，但是栈顶元素X是非终结符
                else if (symbolStackTop.Type == SymbolType.NonTerminal)
                {
                    var ll1Item = analysisTable.FirstOrDefault(v => v.Row.Equals(symbolStackTop) && v.Col.Equals(inputSymbol));

                    //没有找到[symbolStackTop,inputSymbol]
                    if (ll1Item == null)
                    {
                        //X可以归结为ε
                        if (analysisTable.Any(v => v.Row.Equals(symbolStackTop) && v.Value.Right.Contains(Symbols.Epsilon)))
                        {
                            nodeStackTop.Children.Add(new TINYNode("ε"));
                            symbolStack.Pop();
                            NodeStack.Pop();
                        }
                        else
                        {
                            nodeStackTop.Children.Add(new TINYNode(string.Format("Error")));
                            ErrorToken = tokenList[index];
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
                            nodeStackTop.Children.Add(new TINYNode("ε"));
                            continue;
                        }
                        for (int i = ll1Item.Value.Right.Count - 1; i >= 0; i--)
                        {
                            symbolStack.Push(ll1Item.Value.Right[i]);

                            TINYNode newNode = new TINYNode(ll1Item.Value.Right[i].Value);
                            NodeStack.Push(newNode);
                            nodeStackTop.Children.Insert(0, newNode);
                        }
                    }
                }
                else
                {
                    ErrorToken = tokenList[index];
                    return false;
                }
            }
            return true;
        }
    }
}
