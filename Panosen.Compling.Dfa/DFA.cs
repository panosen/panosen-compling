using System.Collections.Generic;

namespace Panosen.Compling
{
    public class DFA
    {
        /// <summary>
        /// 状态集合
        /// </summary>
        public List<TheState> States { get; set; }

        /// <summary>
        /// 状态转移集合
        /// </summary>
        public List<Move> Moves { get; set; }

        /// <summary>
        /// 状态规则映射
        /// </summary>
        public Dictionary<TheState, List<ProductionRule>> StateRuleListMap { get; set; }
    }
}
