using System.Collections.Generic;
using System.Data;

namespace Panosen.Compling
{
    public class AnalyzeResult
    {
        public bool Accepted { get; set; }

        public List<Step> Steps { get; set; }
    }

    public class Step
    {
        public int Id { get; set; }
        public string StateStack { get; set; }
        public string SymbolStack { get; set; }
        public string InputString { get; set; }
        public string Action { get; set; }
    }
}
