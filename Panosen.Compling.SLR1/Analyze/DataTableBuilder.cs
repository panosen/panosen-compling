using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.Compling
{
    /// <summary>
    /// DataTableBuilder
    /// </summary>
    public static class DataTableBuilder
    {
        /// <summary>
        /// BuildDataTable
        /// </summary>
        public static DataTable BuildDataTable(Grammar grammar, Dictionary<TheState, Dictionary<Symbol, TheTableCell>> analysisTable)
        {
            var dict = new Dictionary<TheState, Dictionary<Symbol, TheTableCell>>(analysisTable);

            List<Symbol> symbols = GrammarHelper.GetSymbols(grammar);

            List<Symbol> v_symbols = grammar.Rules.SelectMany(v => v.Right).Where(v => v.Type == SymbolType.Terminal).Distinct().ToList();
            v_symbols.Add(Symbols.Dollar);

            List<Symbol> n_symbols = GetNonTerminals(grammar);

            DataTable table = new DataTable();
            DataColumn column1 = new DataColumn();
            column1.ColumnName = "State";
            column1.DataType = typeof(string);
            table.Columns.Add(column1);

            foreach (var symbol in v_symbols)
            {
                DataColumn action_column = new DataColumn();
                action_column.ColumnName = symbol.ToString1();
                action_column.Caption = "action";
                action_column.DataType = typeof(string);
                table.Columns.Add(action_column);
            }
            foreach (var symbol in n_symbols)
            {
                DataColumn goto_column = new DataColumn();
                goto_column.ColumnName = symbol.ToString1();
                goto_column.DataType = typeof(string);
                table.Columns.Add(goto_column);
            }

            foreach (var key in dict.Keys)
            {
                DataRow row = table.NewRow();
                row["State"] = key.Name;
                foreach (var column in table.Columns)
                {
                    string column_name = column.ToString();
                    if (column_name.Equals("State"))
                    {
                        continue;
                    }
                    var cell = dict[key][symbols.Find(s => s.ToString1().Equals(column_name))];
                    switch (cell.Type)
                    {
                        case TheTableCell.Types.ACC:
                            row[column_name] = "ACC";
                            break;
                        case TheTableCell.Types.REDUCE:
                            row[column_name] = $"R{cell.Value}";
                            break;
                        case TheTableCell.Types.SHIFT:
                            row[column_name] = $"S{cell.Value}";
                            break;
                        case TheTableCell.Types.GOTO:
                            row[column_name] = $"{cell.Value}";
                            break;
                        case TheTableCell.Types.NULL:
                            row[column_name] = "";
                            break;
                    }
                }
                table.Rows.Add(row);
            }

            return table;
        }

        private static List<Symbol> GetNonTerminals(Grammar grammar)
        {
            List<Symbol> temp = new List<Symbol>();
            temp.AddRange(grammar.Rules.Select(v => v.Left));
            temp.AddRange(grammar.Rules.SelectMany(v => v.Right).Where(v => v.Type == SymbolType.NonTerminal));
            return temp.Distinct().ToList();
        }
    }
}
