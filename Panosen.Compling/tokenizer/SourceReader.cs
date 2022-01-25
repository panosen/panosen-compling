using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.Compling
{
    /// <summary>
    /// SourceReader
    /// </summary>
    public class SourceReader
    {
        private int index = 0;

        private string text;

        /// <summary>
        /// Row
        /// </summary>
        public int Row { get; private set; } = 1;

        /// <summary>
        /// Col
        /// </summary>
        public int Col { get; private set; } = 1;

        /// <summary>
        /// SourceReader
        /// </summary>
        public SourceReader(string text)
        {
            this.text = text;
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <returns></returns>
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
                this.Col = 1;
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

        /// <summary>
        /// ViewOne
        /// </summary>
        /// <returns></returns>
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
