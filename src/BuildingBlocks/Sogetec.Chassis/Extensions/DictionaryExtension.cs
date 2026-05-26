namespace Sogetec.Chassis.Extensions;

public static class DictionaryExtension
{
    extension<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
    {
        public TValue? Get(TKey key, TValue? defaultValue = default)
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }
}
