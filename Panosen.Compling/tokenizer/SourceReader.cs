using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Compling
{
    public class SourceReader
    {
        private int index = 0;

        private string text;

        public int Row { get; private set; } = 1;

        public int Col { get; private set; } = 1;

        public SourceReader(string text)
        {
            this.text = text;
        }

        public char? Read()
        {
            if (text == null)
            {
                return null;
            }
            if (index >= text.Length)
            {
                return null;
            }
            char current = text[index];
            if (current == '\r')
            {
                this.Col = 0;
            }
            else if (current == '\n')
            {
                this.Row++;
            }
            else
            {
                this.Col++;
            }
            index++;
            return current;
        }

        public char? ViewOne()
        {
            if (text == null)
            {
                return null;
            }
            if (index >= text.Length)
            {
                return null;
            }
            return text[index];
        }
    }
}
