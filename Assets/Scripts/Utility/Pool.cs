using System.Collections.Generic;
using System.Linq;

namespace Builder.Utility
{
    public class Pool<TKey, TValue>
    {
        private readonly Dictionary<TKey, Stack<TValue>> _dictionary = new();

        public IEnumerable<KeyValuePair<TKey, Stack<TValue>>> Enumerable => _dictionary.AsEnumerable();

        public bool TryGet(TKey key, out TValue value)
        {
            if (!_dictionary.TryGetValue(key, out Stack<TValue> stack))
            {
                _dictionary[key] = new Stack<TValue>();
            }

            if (_dictionary[key].TryPop(out value))
            {
                return true;
            }

            value = default(TValue);
            return false;
        }

        public void Add(TKey key, TValue value)
        {
            if (!_dictionary.TryGetValue(key, out Stack<TValue> stack))
            {
                _dictionary[key] = new Stack<TValue>();
            }
            
            _dictionary[key].Push(value);
        }
    }
}