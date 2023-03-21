using System.Collections;

namespace MyHashTable
{
    public class MyHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        private LinkedList<KeyValuePair<TKey, TValue>>[] _items;
        private const double _overCapacityCoeffecient = 1.5;
        private readonly GetPrimeNumber getPrimeNumber;
        public long Count { get; private set; }

        public MyHashTable()
        {
            getPrimeNumber = new GetPrimeNumber();
            _items = new LinkedList<KeyValuePair<TKey, TValue>>[getPrimeNumber.GetMin()];
        }

        private int GetIndex(TKey key, int arrayLength)
        {
            return (int)((uint)key.GetHashCode() % arrayLength);
        }

        public bool Remove(TKey key)
        {
            var index = GetIndex(key, _items.Length);

            if (_items[index] == null)
                return false;

            var deletingNode = _items[index].FirstOrDefault(x => x.Key.CompareTo(key) == 0);
            if (_items[index].Remove(deletingNode))
            {
                Count--;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Add(TKey key, TValue value)
        {
            AddAtInnerArray(ref _items, key, value);
            Count++;
        }

        private void AddAtInnerArray(ref LinkedList<KeyValuePair<TKey, TValue>>[] innerArray, TKey key, TValue value)
        {
            if (innerArray.Length != Array.MaxLength && Count > innerArray.Length * _overCapacityCoeffecient)
                ExtendArray(ref innerArray);

            var index = GetIndex(key, innerArray.Length);
            var newValue = new KeyValuePair<TKey, TValue>(key, value);
            if (innerArray[index] == null)
            {
                innerArray[index] = new LinkedList<KeyValuePair<TKey, TValue>>();//Инициализируем цепочку
            }
            else
            {
                if (innerArray[index].Contains(newValue))
                {
                    throw new ArgumentException();
                }
            }
            innerArray[index].AddLast(newValue);//Добавляем узел в конец цепочки
        }

        private LinkedListNode<KeyValuePair<TKey, TValue>> GetNodeByKey(TKey key)
        {
            var index = GetIndex(key, _items.Length);
            if (_items[index] == null)
                return null;
            for (var currentNode = _items[index].First; currentNode != null; currentNode=currentNode.Next)
            {
                if (currentNode.Value.Key.Equals(key))
                    return currentNode;
            }
            return null;
        }
        
        public TValue GetValue(TKey key)
        {
            var gettingNode = GetNodeByKey(key);
            if (gettingNode == null)
                throw new ArgumentException();

            return gettingNode.Value.Value;
        }

        public TValue GetValueOrDefault(TKey key, TValue defaultValue)
        {
            var gettingNode = GetNodeByKey(key);
            if (gettingNode == null)
                return defaultValue;

            return gettingNode.Value.Value;
        }

        public void SetValueByKey(TKey key, TValue newValue)
        {
            var gettingNode = GetNodeByKey(key);
            if (gettingNode == null)
                Add(key, newValue);
            else
                gettingNode.Value = new KeyValuePair<TKey, TValue>(key, newValue);
        }

        private void ExtendArray(ref LinkedList<KeyValuePair<TKey, TValue>>[] innerArray)
        {
            int newCapacity = getPrimeNumber.Next();
            if (newCapacity > (uint)Array.MaxLength)
                newCapacity = Array.MaxLength;

            LinkedList<KeyValuePair<TKey, TValue>>[] newArray = 
                new LinkedList<KeyValuePair<TKey, TValue>>[newCapacity];

            foreach (var linkedList in innerArray)
            {
                if (linkedList == null)
                    continue;
                foreach (var item in linkedList)
                {
                    AddAtInnerArray(ref newArray, item.Key, item.Value);
                }
            }
            innerArray = newArray;
        }

        public bool ContainsKey(TKey key)
        {
            return GetNodeByKey(key) != null;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var linkedList in _items)
            {
                if (linkedList == null)
                    continue;
                foreach (var item in linkedList)
                {
                    yield return item;
                }
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TValue this[TKey key]
        {
            get
            {
                var value = GetValue(key);
                return value;
            }
            set
            {
                SetValueByKey(key, value);
            }
        }
    }
}