using System;
using System.Collections.Generic;
using System.Text;

namespace HuffmanCOmpressionEldarVila
{
    public class charFrequency : IComparable<charFrequency>
    {
        public int frequency;
        public char ch;
        public charFrequency left;
        public charFrequency right;
        public string huffmanCode;
        public charFrequency()
        {
            frequency = 0;
            ch = this.ch;
            left = null;
            right = null;
            huffmanCode = "";
        }
        public int CompareTo(charFrequency other)
        {
            return this.frequency.CompareTo(other.frequency);
        }
    }
}
