namespace HashTables
{
    public class HashTableOpen
    {
        private readonly UserData[] _cells;
        private readonly int _size;

        public HashTableOpen(int size)
        {
            _size = size;
            _cells = new UserData[_size];
        }

        /// <summary>
        /// Represents finding an object in a table.
        /// </summary>
        public UserData Search(string id)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(id, i++);
                if (_cells[index] == null || _cells[index].Id != id) continue;
                return _cells[index];
            } while (i < _size);

            return null;
        }

        /// <summary>
        /// Represents adding an object to a table.
        /// </summary>
        public int Add(UserData data)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(data.Id, i++);
                if (_cells[index] is not null) continue;
                _cells[index] = data;
                return index;
            } while (i < _size);

            return -1;
        }

        /// <summary>
        /// Represents deletion of an object to a table.
        /// </summary>
        public bool Remove(string id)
        {
            var i = 0;
            do
            {
                var index = CalculateHash(id, i++);
                if (_cells[index] == null || _cells[i].Id != id) continue;
                _cells[index] = null;
                return true;
            } while (i < _size);

            return false;
        }

        /// <summary>
        /// Represents getting a hash code.
        /// </summary>
        private int CalculateHash(string key, int i) => key[0] - 'a' + i;

        public int GetLargestCluster()
        {
            var i = 0;
            var max = 0;
            foreach (var cell in _cells)
            {
                if (cell == null)
                {
                    i++;
                }
                else
                {
                    if (i > max) max = i;
                    i = 0;
                }
            }

            if (i > max) max = i;
            return max;
        }
    }
}