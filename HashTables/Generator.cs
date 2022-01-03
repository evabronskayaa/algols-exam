using System;
using System.Text;

namespace HashTables
{
    public class Generator
    {
        public static string GenRndCharSeq(int length)
        {
            var rnd = new Random();
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
                sb.Append((char)(rnd.Next(0, 26) + 'a'));
            return sb.ToString();
        }

        public static string GenRndDate() =>
            $"{new Random().Next(1980,2021)}.{new Random().Next(1,13)}.{new Random().Next(1,32)}";
    }
}