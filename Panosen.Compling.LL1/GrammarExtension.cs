using Panosen.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling.LL1
{
    public static class GrammarExtension
    {
        public static bool IsLL1Grammar(this Grammar grammar)
        {
            /*
文法G的任意两个具有相同左部的产生式 A --> α|β 满足下列条件：
1、如果α和β不能同时推导出ε，则 FIRST（α）∩FIRST(β) = 空
2、 α和β 至多有一个能推导出 ε
3、如果 β --*--> ε ,则 FIRST（α)∩ FOLLOW（A）＝ 空 
             */

            var leftSymbols = grammar.Rules.Select(v => v.Left).Distinct().ToList();
            foreach (var leftSymbol in leftSymbols)
            {
                //左边相同的其他产生式
                var productionRules = grammar.Rules.Where(v => v.Left.Equals(leftSymbol)).ToList();
                if (!IsLL1Grammar(grammar, productionRules))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsLL1Grammar(Grammar grammar, List<ProductionRule> productionRules)
        {
            //2、 α和β 至多有一个能推导出 ε
            var selectContainsEpsilonCount = productionRules
                .Select(productionRule => grammar.GetSelect(productionRule))
                .Where(symbols => symbols.Any(v => v.Type == SymbolType.Epsilon))
                .Count();
            if (selectContainsEpsilonCount > 1)
            {
                return false;
            }

            //1、如果α和β不能同时推导出ε，则 FIRST（α）∩FIRST(β) = 空
            HashSet<Symbol> productionRulesFirstSet = new HashSet<Symbol>();
            foreach (var productionRule in productionRules)
            {
                var productionRuleFirstSet = grammar.GetSelect(productionRule);
                if (productionRulesFirstSet.Intersect(productionRuleFirstSet).Count() > 0)
                {
                    return false;
                }
                foreach (var symbol in productionRuleFirstSet)
                {
                    productionRulesFirstSet.Add(symbol);
                }
            }

            return true;
        }

        /// <summary>
        /// 求产生式右部的first集
        /// </summary>
        public static HashSet<Symbol> GetSelect(this Grammar grammar, ProductionRule productionRule)
        {
            HashSet<Symbol> selectSet = new HashSet<Symbol>();

            var firstSetMap = FirstSetMapBuilder.BuildFirstSetMap(grammar);
            var followSetMap = FollowSetMapBuilder.BuildFollowSetMap(grammar);

            // 遍历这个产生式中的每一项
            for (int index = 0; index < productionRule.Right.Count; index++)
            {
                var currentSymbol = productionRule.Right[index];

                //Epsilon
                if (currentSymbol.Type == SymbolType.Epsilon)
                {
                    AddTo(selectSet, followSetMap.GetHashSet(productionRule.Left));
                    break;
                }

                //终结符
                if (currentSymbol.Type == SymbolType.Terminal)
                {
                    selectSet.Add(currentSymbol);
                    break;
                }

                //非终结符
                if (currentSymbol.Type == SymbolType.NonTerminal)
                {
                    var currentSymbolFirstSet = firstSetMap.GetHashSet(currentSymbol);
                    if (currentSymbolFirstSet.Any(v => v.Type == SymbolType.Epsilon))
                    {
                        if (index == productionRule.Right.Count - 1)
                        {
                            AddTo(selectSet, currentSymbolFirstSet);
                        }
                        else
                        {
                            AddTo(selectSet, currentSymbolFirstSet.Where(s => s.Type != SymbolType.Epsilon));
                        }
                    }
                    else
                    {
                        AddTo(selectSet, currentSymbolFirstSet);
                        break;
                    }
                }
            }

            return selectSet;
        }

        private static HashSet<Symbol> GetSet(Dictionary<Symbol, HashSet<Symbol>> setMap, Symbol symbol)
        {
            if (setMap.ContainsKey(symbol))
            {
                return setMap[symbol];
            }

            return new HashSet<Symbol>();
        }

        private static void AddTo(HashSet<Symbol> selectSet, IEnumerable<Symbol> items)
        {
            foreach (var item in items)
            {
                selectSet.Add(item);
            }
        }

        public static Matrix<Symbol, Symbol, ProductionRule> MakeAnalysisTable(this Grammar grammar)
        {
            Matrix<Symbol, Symbol, ProductionRule> ll1Table = new Matrix<Symbol, Symbol, ProductionRule>();

            var firstSetMap = FirstSetMapBuilder.BuildFirstSetMap(grammar);
            var followSetMap = FollowSetMapBuilder.BuildFollowSetMap(grammar);

            foreach (var rule in grammar.Rules)
            {
                //规则包含ε,即 A -> ε
                if (rule.Right.Contains(Symbols.Epsilon))
                {
                    foreach (var follow in followSetMap.GetHashSet(rule.Left))
                    {
                        ll1Table.Add(rule.Left, follow, rule);
                    }
                }

                // A -> bC
                else if (rule.Right[0].Type == SymbolType.NonTerminal)
                {
                    foreach (var first in firstSetMap.GetHashSet(rule.Right[0]))
                    {
                        if (first.Equals(Symbols.Epsilon))
                        {
                            continue;
                        }
                        ll1Table.Add(rule.Left, first, rule);
                    }
                    if (firstSetMap.GetHashSet(rule.Right[0]).Contains(Symbols.Epsilon))
                    {
                        foreach (var follow in followSetMap.GetHashSet(rule.Left))
                        {
                            ll1Table.Add(rule.Left, follow, rule);
                        }
                    }
                }

                // A -> bC
                else
                {
                    ll1Table.Add(rule.Left, rule.Right[0], rule);
                }
            }

            return ll1Table;
        }

        private static HashSet<Symbol> GetFirstSet(Dictionary<Symbol, HashSet<Symbol>> firstSetMap, Symbol symbol)
        {
            if (firstSetMap.ContainsKey(symbol))
            {
                return firstSetMap[symbol];
            }

            return new HashSet<Symbol>();
        }
    }
}
