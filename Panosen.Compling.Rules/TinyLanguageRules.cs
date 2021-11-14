using System;
using System.Collections.Generic;

namespace Panosen.Compling.Rules
{
    public static class TinyLanguageRules
    {
        public static List<ProductionRule> GetRules()
        {
            List<ProductionRule> rules = new List<ProductionRule>();

            var content = @"
Program,STMTSequence
STMTSequence,Statement,STMT_
STMT_,Semicolon,Statement,STMT_
STMT_,ε
Statement,IfSTMT
Statement,RepeatSTMT
Statement,AssignSTMT
Statement,ReadSTMT
Statement,WriteSTMT
IfSTMT,If,Exp,Then,STMTSequence,ElseSTMT,End
ElseSTMT,Else,STMTSequence
ElseSTMT,ε
RepeatSTMT,Repeat,STMTSequence,Until,Exp
AssignSTMT,Identifier,Assign,Exp
ReadSTMT,Read,Identifier
WriteSTMT,Write,Exp
Exp,SimpleExp,CmpExp_
CmpExp_,ComparisionOp,SimpleExp
CmpExp_,ε
ComparisionOp,LessThan
ComparisionOp,Equal
SimpleExp,Term,Term_
Term_,AddOp,Term
Term_,ε
AddOp,Plus
AddOp,Minus
Term,Factor,Factor_
Factor_,MulOp,Factor
Factor_,ε
MulOp,Mul
MulOp,Div
Factor,LeftBracket,Exp,RightBracket
Factor,Identifier
Factor,Number
Number,1
Number,0
Identifier,f
Identifier,x
Semicolon,;
Plus,+
Minus,-
Mul,*
Div,/
Equal,=
If,if
LeftBracket,(
RightBracket,)
Read,read
Then,then
Assign,:=
Until,until
Write,write
End,end
LessThan,<
Repeat,repeat
Else,else
";

            var lines = content.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var items = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                ProductionRule theRule = new ProductionRule();
                theRule.Left = new Symbol { Value = items[0], Type = SymbolType.NonTerminal };

                theRule.Right = new List<Symbol>();
                for (int i = 1; i < items.Length; i++)
                {
                    theRule.Right.Add(new Symbol { Value = items[i] != "ε" ? items[i] : "[Null]", Type = GetSymbolType(items[i]) });
                }

                rules.Add(theRule);
            }

            return rules;
        }

        public static string GetSample()
        {
            return "read x ; if 0 < x then f := 1 ; repeat f := f * x ; x := x - 1 until x = 0 ; write f end";
        }

        private static readonly List<string> characters = new List<string>
        {
            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
            "0","1","2","3","4","5","6","7","8","9",
            "+","-","*","/","=",":",
            ";","(",")","{","}","#"
        };


        private static readonly List<string> keywords = new List<string> { "if", "then", "else", "end", "repeat", "until", "read", "write" };

        private static SymbolType GetSymbolType(string item)
        {
            if (characters.Contains(item))
            {
                return SymbolType.Terminal;
            }
            if (keywords.Contains(item))
            {
                return SymbolType.Terminal;
            }

            switch (item)
            {
                case "+":
                case "-":
                case "0":
                case "1":
                case "*":
                case "/":
                case "(":
                case ")":
                case ";":
                case ":=":
                case "=":
                case "<":
                case "ε":
                    return SymbolType.Terminal;
                default:
                    return SymbolType.NonTerminal;
            }
        }
    }
}
