using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{

    public static class PGrammarExtension
    {
        /// <summary>
        /// 对目标文法生成DFA
        /// </summary>
        public static DFA generateDFA(List<ProductionRule> basicRules)
        {
            List<ProductionRule> pointRules = basicRules.Select(v => new ProductionRule { Left = v.Left, Right = v.Right.Select(x => x).ToList() }).ToList();
            foreach (var pointRule in pointRules)
            {
                pointRule.Right.Insert(0, Symbols.Point);
                if (pointRule.Right.Contains(Symbols.Epsilon))
                {
                    pointRule.Right.Remove(Symbols.Epsilon);
                }
            }

            DFA dfa = new DFA();
            dfa.StateRuleListMap = new Dictionary<TheState, List<ProductionRule>>();

            List<TheState> states = new List<TheState>();
            List<Move> moves = new List<Move>();

            dfa.States = states;
            dfa.Moves = moves;

            #region 初始化第一个状态

            //假设第一条规则是初始非终结符的规则
            var firstPointRule = pointRules.First();

            TheState firstState = new TheState { Name = "I0" };
            dfa.StateRuleListMap[firstState] = GetClosure(pointRules, new List<ProductionRule>() { firstPointRule });
            states.Add(firstState);

            #endregion

            for (int i = 0; i < states.Count; i++)
            {
                List<Symbol> afterP = new List<Symbol>();

                // 遍历规则集中的所有规则，找出点后的所有符号
                if (!dfa.StateRuleListMap.ContainsKey(states[i]))
                {
                    dfa.StateRuleListMap[states[i]] = new List<ProductionRule>();
                }
                foreach (var rule in dfa.StateRuleListMap[states[i]])
                {
                    var pIndex = rule.Right.IndexOf(Symbols.Point);
                    if (pIndex + 1 < rule.Right.Count && !rule.Right[pIndex + 1].Type.Equals("E"))
                    {
                        afterP.Add(rule.Right[pIndex + 1]);
                    }
                }

                // 得到新状态
                foreach (var a in afterP.Distinct())
                {
                    var newRules = moveState(dfa, pointRules, states[i], a);
                    if (newRules.Count > 0)
                    {
                        // 如果新状态已经出现过
                        var findState = states.FirstOrDefault(v => AreSame(dfa.StateRuleListMap[v], newRules));
                        if (!findState.Equals(default(TheState)))
                        {
                            // 更新状态转移表
                            moves.Add(new Move { From = states[i], To = findState, By = a });
                        }
                        // 如果新状态未出现过
                        else
                        {
                            TheState _state = new TheState { Name = $"I{states.Count}" };
                            if (!dfa.StateRuleListMap.ContainsKey(_state))
                            {
                                dfa.StateRuleListMap[_state] = new List<ProductionRule>();
                            }
                            dfa.StateRuleListMap[_state].AddRange(newRules);
                            states.Add(_state);
                            moves.Add(new Move { From = states[i], To = _state, By = a });
                        }
                    }
                }
            }

            return dfa;
        }

        private static bool AreSame(List<ProductionRule> first, List<ProductionRule> second)
        {
            return first.Count == second.Count
                && first.All(v => second.Any(x => v.EqualsTo(x)))
                && second.All(v => first.Any(x => v.EqualsTo(x)));
        }

        /// <summary>
        /// 对目标规则集求闭包，直至集合不再增大
        /// </summary>
        private static List<ProductionRule> GetClosure(List<ProductionRule> pointRules, List<ProductionRule> rules)
        {
            List<ProductionRule> newRules = new List<ProductionRule>();
            newRules.AddRange(rules);

            //TheState state = new TheState();
            //state.Rules.AddRange(rules);
            int ruleCount = -1;

            while (newRules.Count != ruleCount)
            {
                ruleCount = newRules.Count;
                // 遍历目标规则集中的所有规则
                foreach (var newRule in newRules)
                {
                    var pIndex = newRule.Right.IndexOf(Symbols.Point);
                    if (pIndex + 1 >= newRule.Right.Count)
                    {
                        continue;
                    }
                    var symbol = newRule.Right[pIndex + 1];
                    if (symbol.Type == SymbolType.NonTerminal) // 如果是非终结符
                    {
                        //以目标非终结符号为左部的所有产生式集合
                        var rules2 = pointRules.Where(v => v.Left.Equals(symbol)).ToList();

                        //避免重复
                        List<ProductionRule> temp = rules2.Where(v => !newRules.Any(x => v.EqualsTo(x))).ToList();
                        if (temp.Count > 0)
                        {
                            newRules.AddRange(temp);
                            break;
                        }
                    }
                }
            }
            return newRules;
        }


        /// <summary>
        /// 源状态经由输入符号生成转换后的新状态
        /// </summary>
        /// <param name="fromState">原状态</param>
        /// <param name="bySymbol">输入符号</param>
        /// <returns>新状态</returns>
        private static List<ProductionRule> moveState(DFA dfa, List<ProductionRule> pointRules, TheState fromState, Symbol bySymbol)
        {
            List<ProductionRule> to_rules = new List<ProductionRule>();
            foreach (var rule in dfa.StateRuleListMap[fromState])
            {
                var pIndex = rule.Right.IndexOf(Symbols.Point);
                if (pIndex + 1 < rule.Right.Count && rule.Right[pIndex + 1].Equals(bySymbol))
                {
                    var _right = new List<Symbol>(rule.Right.ToArray());

                    var temp = _right[pIndex + 1];
                    _right[pIndex + 1] = _right[pIndex];
                    _right[pIndex] = temp;

                    to_rules.Add(new ProductionRule { Left = rule.Left, Right = _right });
                }
            }
            return GetClosure(pointRules, to_rules);
        }
    }
}
