using HuffmanCOmpressionEldarVila;

Console.Write("Please enter file name(ex. wap.txt, test.txt: ");
string openFile = Console.ReadLine();
string wapFile = File.ReadAllText(openFile);
List<charFrequency> charFreq = new List<charFrequency>();
List<char> charList = new List<char>();
foreach (char c in wapFile)
{
    charFrequency charFrequency = new charFrequency();
    if (!charList.Contains(c))
    {
        charList.Add(c);
        charFrequency.ch = c;
        charFrequency.frequency = 0;
        charFreq.Add(charFrequency);
    }
    foreach (charFrequency cf in charFreq)
    {
        if (cf.ch == c)
        {
            cf.frequency++;
        }
    }
}

huffmanTree<charFrequency> ht = new huffmanTree<charFrequency>();
ht.list = charFreq;
ht.sorting(ht.list);
ht.huffmanTreeCreation();
ht.huffmanEncode(ht.sortedList[0], "");

foreach(char c in wapFile)
{
    foreach(charFrequency cf in ht.nodes)
    {
        if (cf.ch == c)
        {
            foreach(char bin in cf.huffmanCode)
            {
                if (bin == '0')
                {
                    ht.byteTransfer(0);
                }
                else
                {
                    ht.byteTransfer(1);
                }
            }
        }
    }
}

if (ht.count > 0)
{
    ht.currentByte <<= (8 - ht.count);
    ht.byteList.Add(ht.currentByte);
    ht.extraBits++;
}
File.WriteAllBytes("compression.bin", ht.byteList.ToArray());

ht.decompress(ht.sortedList[0]);