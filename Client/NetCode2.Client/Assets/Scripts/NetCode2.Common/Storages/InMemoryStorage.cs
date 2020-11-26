using System.Collections;
using System.Collections.Generic;
using NetCode2.Common.Storages.Contracts;

namespace NetCode2.Common.Storages
{
    public class InMemoryStorage<TKey, TValue> : IStorage<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> dictionary;

        public InMemoryStorage()
        {
            dictionary = new Dictionary<TKey, TValue>();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(TKey key, TValue value) => dictionary.Add(key, value);

        public bool Contains(TKey key) => dictionary.ContainsKey(key);

        public void Remove(TKey key) => dictionary.Remove(key);

        public TValue Get(TKey key) => dictionary[key];

        public bool TryGet(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);

        public int Count() => dictionary.Count;
    }
}