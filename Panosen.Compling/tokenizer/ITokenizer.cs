namespace Panosen.Compling
{
    /// <summary>
    /// 词法分析
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        /// 此法分析
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        TokenCollection Analyze(string text);
    }
}