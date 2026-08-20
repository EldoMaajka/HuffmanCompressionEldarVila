using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Threading.Channels;

namespace HuffmanCOmpressionEldarVila
{
    public class huffmanTree<T>
    {
        public List<charFrequency> list = new List<charFrequency>();
        public charFrequency rootNode;
        public List<charFrequency> sortedList = new List<charFrequency>();
        public List<charFrequency> nodes = new List<charFrequency>();

        public List<byte> byteList = new List<byte>();
        public byte currentByte = 0;
        public int count = 0;
        public int extraBits = 0;
        public void sorting(List<charFrequency> chosenList)
        {
            charFrequency max = new charFrequency();
            List<charFrequency> preSortedList = chosenList;


            while (preSortedList.Count >= 1)
            {
                max = null;
                foreach (charFrequency frequency in preSortedList)
                {
                    if (max == null)
                    {
                        max = frequency;
                    }
                    if (max.frequency > frequency.frequency)
                    {
                        max = frequency;
                    }
                }
                preSortedList.Remove(max);
                sortedList.Add(max);

            }

            Console.WriteLine(sortedList.Count.ToString());
            charFrequency currentNode = null;

        }

        public void huffmanTreeCreation()
        {
            while (sortedList.Count > 1)
            {
                charFrequency left = sortedList[0];
                charFrequency right = sortedList[1];
                charFrequency parent = new charFrequency();

                parent.left = left;
                parent.right = right;
                parent.frequency = left.frequency + right.frequency;

                sortedList.RemoveAt(0);
                sortedList.RemoveAt(0);

                sortedList.Add(parent);
                sortedList.Sort();
            }
        }
        

        public void huffmanEncode(charFrequency cf, string huffCode)
        {
            if (cf == null)
            {
                return;
            }
            if ((cf.left == null) && (cf.right == null))
            {
                cf.huffmanCode = huffCode;
                nodes.Add(cf);
            }

            huffmanEncode(cf.left, huffCode + "0");
            huffmanEncode(cf.right, huffCode + "1");
        }

        public void byteTransfer(int i)
        {
            currentByte <<= 1;

            if (i == 1)
            {
                currentByte |= 1;
            }
            else
            {
                currentByte |= 0;
            }
            count++;
            if (count == 8)
            {
                byteList.Add(currentByte);
                currentByte = 0;
                count = 0;
                i = 0;
            }
        }
        public void decompress(charFrequency root)
        {
            List<char> decompressed = new List<char>();
            charFrequency currentNode = root;
            int decompressionCount = 0;
            foreach (byte b in byteList)
            {
                for (int i = 7; i >= 0; i--)
                {
                    int j = (b >> i) & 1;
                    if (j  == 0)
                    {
                        currentNode = currentNode.left;
                    }
                    else
                    {
                        currentNode = currentNode.right;
                    }
                    if ((currentNode.left == null) && (currentNode.right == null))
                    {
                        decompressed.Add(currentNode.ch);
                        currentNode = root;
                        decompressionCount++;
                    }
                    if (decompressionCount == root.frequency)
                    {
                        break;
                    }
                }

            }
            File.WriteAllText("decompressed.txt", decompressed.ToArray());
        }
    }
}
