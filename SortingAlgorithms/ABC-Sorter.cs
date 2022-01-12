using System.Collections.Generic;

namespace SortingAlgorithms
{
    public class ABC_Sorter
    {
        private static int?[] _indexes;
        private static List<int?[]> _level;
        private static List<string> _result;
        private static string[] _array;

        public ABC_Sorter (string[] array)
        {
            _array = array;
            _indexes = new int?[_array.Length];
            _level = new List<int?[]> {new int?[26]};  // 26 букв лат, на опр уровне сравниваются слова aka RadixSort
            _result = new List<string>();
        }

        public string[] ABCSort()
        {
            for (var i = 0; i < _array.Length; i++)
            {
                var letter = char.ToUpper(_array[i][0]) - 65; // вычисляется номер буквы
                _indexes[i] = _level[0][letter];
                _level[0][letter] = i;
            }

            ClearLevel(0);

            return _result.ToArray();
        }

        private static void ClearLevel(int depth)
        {
            if (_level.Count == depth + 1) _level.Add(new int?[26]);

            for (var i = 0; i < 26; i++)
            {
                if (_level[depth][i] != null)
                {
                    var pos = _level[depth][i].GetValueOrDefault();
                    if (_indexes[pos] == null)
                    {
                        _result.Add(_array[pos]);
                        _level[depth][i] = null;
                    }
                    else
                    {
                        MarkChain(pos, depth);
                        _level[depth][i] = null;
                        ClearLevel(depth + 1);
                    }
                }
            }
        }

        private static void MarkChain(int pos, int depth)
        {
            while (true)
            {
                var nextPos = _indexes[pos];

                if (depth + 1 >= _array[pos].Length)
                {
                    _result.Add(_array[pos]);
                    _indexes[pos] = null;
                }
                else
                {
                    int letter = char.ToUpper(_array[pos][depth + 1]) - 65;
                    _indexes[pos] = _level[depth + 1][letter];
                    _level[depth + 1][letter] = pos;
                }

                if (nextPos == null) break;
                else pos = nextPos.GetValueOrDefault();
            }
        }
    }
}