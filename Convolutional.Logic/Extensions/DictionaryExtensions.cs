using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Convolutional.Logic.Extensions
{
    public static class DictionaryExtensions
    {
        public static IReadOnlyDictionary<TKey, TValue> ToReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dict);
        }
    }
}