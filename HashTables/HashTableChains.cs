using System;
using System.Collections.Generic;

namespace HashTables
{
    public class HashTableChains
    {
        private List<UserData>[] cells;

        public HashTableChains(int size)
        {
            cells = new List<UserData>[size];
        }

        private int CalculateHash(string key)
        {
            int hash = 1;
            for (int i = 0; i < key.Length / 2; i++)
                hash += key[i] - 'a' + 1;
            //hash *= key[i] - 'a' + 1;
            //hash = key[0] - 'a';
            return hash % cells.Length;
        }

        public UserData Search(string id) => cells[CalculateHash(id)].Find(t => t.Id == id);


        public int Add(UserData data)
        {
            var index = CalculateHash(data.Id);
            if (cells[index] is null) cells[index] = new List<UserData>();
            cells[index].Insert(0, data);
            return index;
        }

        /// <summary>
        /// Represents deletion of an object to a table.
        /// </summary>
        public bool Remove(string id)
        {
            _ = cells[CalculateHash(id)].Remove(cells[CalculateHash(id)].Find(t => t.Id == id));
            return true;
        }

        public Tuple<int, int, int> Calculations()
        {
            int maxValue = 0;
            int minValue = int.MaxValue;
            int elements = 0;
            foreach (var t in cells)
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