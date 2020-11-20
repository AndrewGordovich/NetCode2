using System.Collections.Generic;

namespace NetCode2.Common.Storages.Contracts
{
    public interface IStorage<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        void Add(TKey key, TValue value);

        bool Contains(TKey key);

        void Remove(TKey key);

        TValue Get(TKey key);

        bool TryGet(TKey key, out TValue value);

        int Count();
    }
}