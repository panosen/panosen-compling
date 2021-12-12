namespace Panosen.Compling
{
    public enum SymbolType
    {
        /// <summary>
        /// 未设置
        /// </summary>
        None,

        /// <summary>
        /// 非终结符
        /// </summary>
        NonTerminal,

        /// <summary>
        /// 终结符
        /// </summary>
        Terminal,

        /// <summary>
        /// 空串
        /// </summary>
        Epsilon
    }
}
