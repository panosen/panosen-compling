using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    public class AnalyserExtension
    {
        /// <summary>
        /// 将输入字符串转化成符号队列
        /// </summary>
        /// <param name="sInput">输入字符串</param>
        /// <param name="qInput">符号队列</param>
        private static Queue<Symbol> StringToQueue(string sInput, Grammar grammar)
        {
            Queue<Symbol> qInput = new Queue<Symbol>();
            foreach (string s in sInput.Trim().Split(' '))
            {
                var symbol = grammar.Symbols.FirstOrDefault(v => v.Value == s);
                if (!symbol.Equals(default(Symbol)))
                {
                    qInput.Enqueue(symbol);
                    continue;
                }

                throw new Exception($"输入了未定义的符号 {s}");
            }
            qInput.Enqueue(Symbols.Dollar);

            return qInput;
        }

        /// <summary>
        /// 分析输入串是否符合文法
        /// </summary>
        /// <param name="v">输入串</param>
        /// <param name="output">输出数据表</param>
        /// <returns>返回是否分析成功</returns>
        public static AnalyzeResult analyseInput(string inputString, DFA dfa, Dictionary<TheState, Dictionary<Symbol, TheTableCell>> analysisTable, Grammar grammar)
        {
            Queue<Symbol> inputQueue = StringToQueue(inputString, grammar);

            AnalyzeResult myClass = new AnalyzeResult();

            int stepNumber = 1;
            Stack<TheState> stateStack = new Stack<TheState>();
            Stack<Symbol> symbolStack = new Stack<Symbol>();
            List<string> actionList = new List<string>();
            bool isAccepted = false;
            bool isEnded = false;

            List<Step> steps = new List<Step>();

            symbolStack.Push(Symbols.Dollar);
            stateStack.Push(dfa.States.Find(t => t.Name == "I0"));

            while (true)
            {
                Symbol inputSymbol = inputQueue.Peek();
                TheState topState = stateStack.Peek();
                TheTableCell cell = analysisTable[topState][inputSymbol];
                if (isEnded)
                {
                    if (isAccepted)
                    {
                        actionList.Add("ACCEPTED");
                    }
                    else
                    {
                        actionList.Add("NOT ACCEPTED");
                    }
                    break;
                }
                Step step = new Step();
                step.Id = stepNumber;

                StringBuilder sbState = new StringBuilder();
                foreach (var state in stateStack.Reverse())
                {
                    sbState.Append(state.Name);
                }
                step.StateStack = sbState.ToString();

                StringBuilder sbSymbol = new StringBuilder();
                foreach (var symbol in symbolStack.Reverse())
                {
                    sbSymbol.Append(symbol.ToString1());
                }
                step.SymbolStack = sbSymbol.ToString();

                StringBuilder sbInput = new StringBuilder();
                foreach (var i in inputQueue)
                {
                    sbInput.Append(i.ToString1());
                }
                step.InputString = sbInput.ToString();
                steps.Add(step);

                switch (cell.Type)
                {
                    case TheTableCell.Types.SHIFT:
                        stateStack.Push(dfa.States[cell.Value]);
                        symbolStack.Push(inputSymbol);
                        inputQueue.Dequeue();
                        actionList.Add(cell.ToString2());
                        stepNumber++;
                        break;
                    case TheTableCell.Types.REDUCE:
                        ProductionRule rule = grammar.Rules[cell.Value];
                        if (rule.Right[0].Equals(Symbols.Epsilon))
                        {
                            symbolStack.Push(Symbols.Epsilon);
                            stateStack.Push(stateStack.Peek());
                        }
                        for (int i = 0; i < rule.Right.Count; i++)
                        {
                            symbolStack.Pop();
                            stateStack.Pop();
                        }
                        symbolStack.Push(rule.Left);
                        var nextState = stateStack.Peek();
                        var nextSymbol = symbolStack.Peek();
                        var _cell = analysisTable[nextState][nextSymbol];
                        if (_cell.Type != TheTableCell.Types.GOTO)
                        {
                            isEnded = true;
                        }
                        else
                        {
                            stateStack.Push(dfa.States[_cell.Value]);
                        }
                        actionList.Add(cell.ToString2());
                        stepNumber++;
                        break;
                    case TheTableCell.Types.ACC:
                        isEnded = true;
                        isAccepted = true;
                        stepNumber++;
                        break;
                    default:
                        isEnded = true;
                        stepNumber++;
                        break;
                }
            }

            for (int i = 0; i < steps.Count; i++)
            {
                steps[i].Action = actionList[i];
            }

            myClass.Steps = steps;
            myClass.Accepted = isAccepted;

            return myClass;
        }
    }
}
