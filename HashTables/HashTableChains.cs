using System;
using System.Collections.Generic;

namespace HashTables
{
    public class HashTableChains
    {
        private readonly List<UserData>[] _cells;

        public HashTableChains(int size)
        {
            _cells = new List<UserData>[size];
        }

        private int CalculateHash(string key)
        {
            int hash = 1;
            for (int i = 0; i < key.Length / 2; i++)
                hash += key[i] - 'a' + 1;
            return hash % _cells.Length;
        }

        public UserData Search(string id) => _cells[CalculateHash(id)].Find(t => t.Id == id);

        public int Add(UserData data)
        {
            var index = CalculateHash(data.Id);
            if (_cells[index] is null) _cells[index] = new List<UserData>();
            _cells[index].Insert(0, data);
            return index;
        }

        /// <summary>
        /// Represents deletion of an object to a table.
        /// </summary>
        public bool Remove(string id)
        {
            _ = _cells[CalculateHash(id)].Remove(_cells[CalculateHash(id)].Find(t => t.Id == id));
            return true;
        }

        public Tuple<int, int, int> Calculations()
        {
            int maxValue = 0;
            int minValue = int.MaxValue;
            int elements = 0;
            foreach (var t in _cells)
            {
                if (t is null) continue;
                elements++;
                maxValue = t.Count > maxValue ? t.Count : maxValue;
                minValue = t.Count < minValue ? t.Count : minValue;
            }

            return new Tuple<int, int, int>(maxValue, minValue, elements);
        }
    }
}