using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.SLR1
{
    /// <summary>
    /// AnalysisTableBuilder
    /// </summary>
    public static class AnalysisTableBuilder
    {
        /// <summary>
        /// 生成分析表
        /// </summary>
        public static Dictionary<TheState, Dictionary<Symbol, TheTableCell>> generateAnalysisTable(Grammar grammar, DFA dfa)
        {
            Dictionary<TheState, Dictionary<Symbol, TheTableCell>> analysisTable = new Dictionary<TheState, Dictionary<Symbol, TheTableCell>>();

            var followSetMap = FollowSetMapBuilder.BuildFollowSetMap(grammar);

            // 先处理无转移状态的情况
            foreach (var dfaState in dfa.States)
            {
                ProcessState(dfa, grammar, analysisTable, dfaState, followSetMap);
            }

            // 遍历转移表
            foreach (var dfaMove in dfa.Moves)
            {
                ProcessMove(dfa, grammar, dfa.States, analysisTable, dfaMove, followSetMap);
            }

            // (I1, $) 在分析表上对应ACCEPT
            analysisTable[dfa.States[1]][Symbols.Dollar].Type = TheTableCell.Types.ACC;

            return analysisTable;
        }

        private static void ProcessState(DFA dfa, Grammar grammar, Dictionary<TheState, Dictionary<Symbol, TheTableCell>> analysisTable, TheState dfaState, SymbolHashSetMap followSetMap)
        {
            // 初始化 analysisTable
            List<Symbol> symbols = GrammarHelper.GetSymbols(grammar).Where(v => !v.Equals(Symbols.Epsilon)).ToList();

            analysisTable.Add(dfaState, new Dictionary<Symbol, TheTableCell>());
            foreach (var symbol in symbols)
            {
                analysisTable[dfaState][symbol] = new TheTableCell();
            }

            // 如果 [Point] 在其中一个状态的所有产生式右部的最后
            if (!dfa.StateRuleListMap.ContainsKey(dfaState))
            {
                dfa.StateRuleListMap[dfaState] = new List<ProductionRule>();
            }
            if (dfa.StateRuleListMap[dfaState].All(r => r.Right.Last().Equals(Symbols.Point)))
            {
                // 遍历该状态下的全部产生式
                foreach (var rule in dfa.StateRuleListMap[dfaState])
                {
                    ProductionRule _rule = new ProductionRule();
                    _rule.Left = rule.Left;
                    _rule.Right = rule.Right.Where(v => !Symbols.Point.Equals(v)).Select(v => v).ToList();
                    if (_rule.Right.Count == 0)
                    {
                        _rule.Right.Add(Symbols.Epsilon);
                    }
                    // 找到这条产生式的编号
                    var index = grammar.Rules.FindIndex(r => r.EqualsTo(_rule));
                    var followSet = followSetMap.GetHashSet(rule.Left); // 求出这条产生式左部的FOLLOW集
                    foreach (var follow in followSet) // 在相应位置上标记为规约
                    {
                        analysisTable[dfaState][follow].Type = TheTableCell.Types.REDUCE;
                        analysisTable[dfaState][follow].Value = index;
                    }
                }
            }
        }

        private static void ProcessMove(DFA dfa, Grammar grammar, List<TheState> dfaStates, Dictionary<TheState, Dictionary<Symbol, TheTableCell>> analysisTable, Move dfaMove, SymbolHashSetMap followSetMap)
        {
            switch (dfaMove.By.Type)
            {
                // 如果转移条件是终结符
                case SymbolType.Terminal:
                    // 对于源状态不含有 [Point] 在产生式右部的最后的规则，标记为移进
                    foreach (var rule in dfa.StateRuleListMap[dfaMove.From].Where(r => !r.Right.Last().Equals(Symbols.Point)))
                    {
                        analysisTable[dfaMove.From][dfaMove.By].Type = TheTableCell.Types.SHIFT;
                        analysisTable[dfaMove.From][dfaMove.By].Value = dfaStates.IndexOf(dfaMove.To);
                    }
                    // 对于源状态含有 [Point] 在产生式右部的最后的规则，标记为规约
                    foreach (var rule in dfa.StateRuleListMap[dfaMove.From].Where(r => r.Right.Last().Equals(Symbols.Point)))
                    {
                        ProductionRule _rule = new ProductionRule();
                        _rule.Left = rule.Left;
                        _rule.Right = rule.Right.Where(v => !Symbols.Point.Equals(v)).Select(v => v).ToList();
                        if (_rule.Right.Count == 0)
                        {
                            _rule.Right.Add(Symbols.Epsilon);
                        }
                        var index = grammar.Rules.FindIndex(t => t.EqualsTo(_rule));
                        HashSet<Symbol> followSet = followSetMap.GetHashSet(rule.Left);
                        foreach (var follow in followSet)
                        {
                            if (analysisTable[dfaMove.From][follow].Type != TheTableCell.Types.NULL) // 产生冲突
                            {
                                throw new Exception("该文法不是SLR(1)文法");
                            }
                            analysisTable[dfaMove.From][follow].Type = TheTableCell.Types.REDUCE;
                            analysisTable[dfaMove.From][follow].Value = index;
                        }
                    }
                    break;
                // 如果转移条件是非终结符，它对应GO TO表
                case SymbolType.NonTerminal:
                    analysisTable[dfaMove.From][dfaMove.By].Type = TheTableCell.Types.GOTO;
                    analysisTable[dfaMove.From][dfaMove.By].Value = dfaStates.IndexOf(dfaMove.To);
                    break;
                default:
                    analysisTable[dfaMove.From][dfaMove.By].Type = TheTableCell.Types.NULL;
                    break;
            }
        }
    }
}
