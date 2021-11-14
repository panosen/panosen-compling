using Microsoft.Win32;
using Panosen.Compling.Rules;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Panosen.Compling;
using System.Linq;
using System.IO;
using Panosen.Compling.SLR1;

namespace Panosen.Compling.Display
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 文法
        /// </summary>
        private Grammar grammar;

        private DFA dfa;

        private Dictionary<TheState, Dictionary<Symbol, TheTableCell>> analysisTable;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCase1_Click(object sender, RoutedEventArgs e)
        {
            Prepare(TestRules1.GetRules());

            this.tbInput.Text = "i * ( ( ( i * i * i ) + i * ( i * i ) ) * i ) * i";
        }

        private void BtnCase2_Click(object sender, RoutedEventArgs e)
        {
            Prepare(TestRules2.GetRules());

            this.tbInput.Text = "i + i";
        }

        private void BtnCase3_Click(object sender, RoutedEventArgs e)
        {
            Prepare(TestRules3.GetRules());

            this.tbInput.Text = "id * id + id * id";
        }

        private void BtnCase4_Click(object sender, RoutedEventArgs e)
        {
            Prepare(TestRules4.GetRules());
        }

        private void BtnCase5_Click(object sender, RoutedEventArgs e)
        {
            Prepare(TestRules5.GetRules());

            this.tbInput.Text = TestRules5.GetSample();
        }

        private void BtnCase6_Click(object sender, RoutedEventArgs e)
        {
            Prepare(TestRules6.GetRules());

            this.tbInput.Text = TestRules6.GetSample();
        }

        private void BtnCase7_Click(object sender, RoutedEventArgs e)
        {
            Prepare(TinyLanguageRules.GetRules());

            this.tbInput.Text = TinyLanguageRules.GetSample();
        }

        private void Prepare(List<ProductionRule> rules)
        {
            grammar = new Grammar { Rules = rules };

            //DFA
            dfa = PGrammarExtension.generateDFA(grammar.Rules);

            //分析表
            analysisTable = AnalysisTableBuilder.generateAnalysisTable(grammar, dfa);

            listviewN.Items.Clear();
            listviewV.Items.Clear();
            listviewP.Items.Clear();
            listviewFirst.Items.Clear();
            listviewFollow.Items.Clear();
            listviewStates.Items.Clear();
            listviewMoves.Items.Clear();
            datagrid.Children.Clear();
            datagrid.RowDefinitions.Clear();
            datagrid.ColumnDefinitions.Clear();

            foreach (var rule in grammar.Rules)
            {
                listviewP.Items.Add(new ListViewItem() { Content = rule.ToString1() });
            }
            foreach (var sym in grammar.NonTerminals)
            {
                listviewN.Items.Add(new ListViewItem() { Content = sym.ToString1() });
            }
            foreach (var sym in grammar.Terminals)
            {
                listviewV.Items.Add(new ListViewItem() { Content = sym.ToString1() });
            }
            foreach (var symbol in grammar.NonTerminals)
            {
                listviewFirst.Items.Add(new { Sym = symbol.ToString1(), Str = ListToString(grammar.GetFirstSet(symbol)) });
            }
            foreach (var symbol in grammar.NonTerminals)
            {
                listviewFollow.Items.Add(new { Sym = symbol.ToString1(), Str = ListToString(grammar.GetFollowSet(symbol)) });
            }



#if !DEBUG
            {
                List<string> newLines = new List<string>();
                var lines = File.ReadAllLines(@"D:\Github\harris2012\TinyAnalyse\TinyLibrary\Resources\TINYFirst.txt");
                foreach (var line in lines)
                {
                    var items = line.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                    var left = items[0];
                    var rights = items.Skip(1).OrderBy(v => v).ToList();
                    newLines.Add($"{left},{string.Join(",", rights)}");
                }
                newLines = newLines.OrderBy(v => v).ToList();
                File.WriteAllLines(@"D:\Github\harris2012\TinyAnalyse\TinyLibrary\Resources\TINYFirst.txt", newLines, Encoding.UTF8);
            }
            {
                List<string> newLines = new List<string>();
                var lines = File.ReadAllLines(@"D:\Github\harris2012\TinyAnalyse\TinyLibrary\Resources\TINYFollow.txt");
                foreach (var line in lines)
                {
                    var items = line.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
                    var left = items[0];
                    var rights = items.Skip(1).OrderBy(v => v).ToList();
                    newLines.Add($"{left},{string.Join(",", rights)}");
                }
                newLines = newLines.OrderBy(v => v).ToList();
                File.WriteAllLines(@"D:\Github\harris2012\TinyAnalyse\TinyLibrary\Resources\TINYFollow.txt", newLines, Encoding.UTF8);
            }

            {
                List<string> newLines = new List<string>();
                foreach (var symbol in grammar.NonTerminals)
                {
                    var rights = grammar.GetFirst(symbol).Select(v => v.ToString1()).OrderBy(v => v).ToList();

                    newLines.Add($"{symbol.ToString1()},{string.Join(",", rights)}");
                }
                newLines = newLines.OrderBy(v => v).ToList();
                File.WriteAllLines(@"D:\Github\harris2012\TinyAnalyse\TinyLibrary\Resources\TINYFirst-2.txt", newLines, Encoding.UTF8);
            }
            {
                List<string> newLines = new List<string>();
                foreach (var symbol in grammar.NonTerminals)
                {
                    var rights = grammar.GetFollow(symbol).Select(v => v.ToString1()).OrderBy(v => v).ToList();

                    newLines.Add($"{symbol.ToString1()},{string.Join(",", rights)}");
                }
                newLines = newLines.OrderBy(v => v).ToList();
                File.WriteAllLines(@"D:\Github\harris2012\TinyAnalyse\TinyLibrary\Resources\TINYFollow-2.txt", newLines, Encoding.UTF8);
            } 
#endif



            foreach (var state in dfa.States)
            {
                listviewStates.Items.Add(new { Name = state.Name, Productions = string.Join("  ", dfa.StateRuleListMap[state].Select(v => v.ToString1())) });
            }
            foreach (var move in dfa.Moves)
            {
                listviewMoves.Items.Add(new { From = move.From.Name, By = move.By.ToString1(), To = move.To.Name });
            }

            #region Show Table

            DataTable table = DataTableBuilder.BuildDataTable(grammar, analysisTable);

            BindDataTableToGrid(table, datagrid);
            #endregion
        }

        private string ListToString(HashSet<Symbol> symbols)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var symbol in symbols)
            {
                sb.Append(symbol.ToString1() + "  ");
            }
            return sb.ToString();
        }

        private void btnAnalyzeInput_Click(object sender, RoutedEventArgs e)
        {
            if (grammar == null)
            {
                return;
            }

            var analyseResult = AnalyserExtension.analyseInput(tbInput.Text, dfa, analysisTable, grammar);
            var isAccepted = analyseResult.Accepted;

            listViewSteps.Items.Clear();
            foreach (var item in analyseResult.Steps)
            {
                listViewSteps.Items.Add(item);
            }

            tbResult.Text = isAccepted ? "输入串符合文法" : "输入串不符合文法";
        }

        private static void BindDataTableToGrid(DataTable table, Grid grid)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            for (int col = 0; col < table.Columns.Count; col++)
            {
                TextBlock textBlock = new TextBlock();
                grid.Children.Add(textBlock);
                Grid.SetRow(textBlock, 0);
                Grid.SetColumn(textBlock, col);
                textBlock.Text = table.Columns[col].ColumnName;
            }

            for (int row = 0; row < table.Rows.Count; row++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int col = 0; col < table.Columns.Count; col++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    TextBlock textBlock = new TextBlock();
                    grid.Children.Add(textBlock);
                    Grid.SetRow(textBlock, row + 1);
                    Grid.SetColumn(textBlock, col);
                    textBlock.Text = table.Rows[row][col].ToString();
                }
            }
        }
    }
}
