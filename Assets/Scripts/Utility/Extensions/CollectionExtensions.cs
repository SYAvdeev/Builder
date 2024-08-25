using System;
using System.Collections.Generic;
using System.Linq;

namespace Builder.Utility.Extensions
{
    public enum SortingOrder
    {
        Unsorted,

        Ascendant,
        Descendant
    }

    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return list.Count == 0;
        }
        
        public static T[] Add<T>(this T[] target, T item)
        {
            T[] result;

            if (target == null)
            {
                result = new[] {item};
            }
            else
            {
                result = new T[target.Length + 1];
                target.CopyTo(result, 0);
                result[target.Length] = item;
            }

            return result;
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValue;
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> defaultValueProvider)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : defaultValueProvider();
        }

        public static T Get<T>(this IList<T> list, int index)
        {
            return index >= 0 && index < list.Count ? list[index] : default;
        }

        public static T Get<T>(this IList<T> list, int index, T fallback)
        {
            return index >= 0 && index < list.Count ? list[index] : fallback;
        }

        public static T Peek<T>(this List<T> list)
        {
            T item = default;

            if (list.Count > 0)
            {
                item = list[list.Count - 1];
            }

            return item;
        }

        public static T Pop<T>(this List<T> list, int index = -1)
        {
            T item = default;

            if (list.Count > 0)
            {
                index = index < 0 ? list.Count - 1 : index;
                item = list[index];
                list.RemoveAt(index);
            }

            return item;
        }

        public static T[] Shuffle<T>(this T[] array, int length = -1)
        {
            length = length < 0 ? array.Length : length;

            for (int index = 0; index < length - 1; index++)
            {
                int r = UnityEngine.Random.Range(index, length);
                (array[index], array[r]) = (array[r], array[index]);
            }

            return array;
        }

        public static T[] Shuffle<T>(this T[] array, Random random, int count = -1)
        {
            count = count < 0 ? array.Length : count;

            for (int index = 0; index < count - 1; index++)
            {
                int r = random.Next(index, count);
                (array[index], array[r]) = (array[r], array[index]);
            }

            return array;
        }

        public static T[] Shuffle<T>(this T[] array, Random random, int from, int count)
        {
            for (int index = from, length = index + count; index < length - 1; ++index)
            {
                int r = random.Next(from, length);
                (array[index], array[r]) = (array[r], array[index]);
            }

            return array;
        }

        public static List<T> Shuffle<T>(this List<T> list)
        {
            for (int index = 0; index < list.Count - 1; index++)
            {
                int r = UnityEngine.Random.Range(index, list.Count);
                (list[index], list[r]) = (list[r], list[index]);
            }

            return list;
        }

        public static List<T> Shuffle<T>(this List<T> list, Random random)
        {
            for (int index = 0; index < list.Count - 1; index++)
            {
                int r = random.Next(index, list.Count);
                (list[index], list[r]) = (list[r], list[index]);
            }

            return list;
        }

        public static List<T> Shuffle<T>(this List<T> list, int from, int count)
        {
            for (int index = from, length = index + count; index < length - 1; ++index)
            {
                int r = UnityEngine.Random.Range(from, length);
                (list[index], list[r]) = (list[r], list[index]);
            }

            return list;
        }

        public static List<T> Unique<T>(this List<T> list)
        {
            if (list == null)
            {
                return default;
            }

            List<T> unique = new List<T>();

            foreach (T element in list)
            {
                if (!unique.Contains(element))
                {
                    unique.Add(element);
                }
            }

            return unique;
        }

        public static List<int> FindAllIndexes<T>(this List<T> list, Predicate<T> predicate)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                T element = list[i];
                if (predicate(element))
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }

        public static T Random<T>(this IList<T> list)
        {
            return list.Random(out _);
        }

        public static T Random<T>(this IList<T> list, out int index)
        {
            index = UnityEngine.Random.Range(0, list.Count);
            return list.IsNullOrEmpty() ? default : list[index];
        }

        public static T Random<T>(this IList<T> list, Random random)
        {
            return list.IsNullOrEmpty() ? default : list[random.Next(0, list.Count)];
        }

        public static T Random<T>(this IList<T> list, IEnumerable<T> exclude)
        {
            if (list.IsNullOrEmpty())
            {
                return default;
            }

            if (exclude != null)
            {
                list = list.Except(exclude).ToList();
            }

            return list.Random();
        }

        public static T Random<T>(this IList<T> list, Func<T, float> weight)
        {
            if (list.IsNullOrEmpty())
            {
                return default;
            }

            float target = UnityEngine.Random.Range(0, list.Sum(weight));

            foreach (var element in list)
            {
                float w = weight.Invoke(element);

                if (w > target)
                {
                    return element;
                }

                target -= w;
            }

            return list[list.Count - 1];
        }

        public static T Random<T>(this IList<T> list, Func<T, float> weight, Random random)
        {
            if (list.IsNullOrEmpty())
            {
                return default;
            }

            float target = (float) random.NextDouble() * list.Sum(weight);

            foreach (var element in list)
            {
                float w = weight.Invoke(element);

                if (w > target)
                {
                    return element;
                }

                target -= w;
            }

            return list[list.Count - 1];
        }

        public static T Random<T>(this HashSet<T> set, Func<T, float> weight)
        {
            if (set == null || set.Count == 0)
            {
                return default;
            }

            float target = UnityEngine.Random.Range(0, set.Sum(weight));
            float sum = 0;

            return set.FirstOrDefault(element =>
            {
                sum += weight(element);

                return sum >= target;
            });
        }

        public static void Move<T>(this List<T> list, int from, int to)
        {
            if (from == to || from < 0 || from >= list.Count || to < 0 || to >= list.Count)
            {
                return;
            }

            int index;
            T tmp = list[from];
            if (from < to)
            {
                for (index = from; index < to; index++)
                {
                    list[index] = list[index + 1];
                }
            }
            else
            {
                for (index = from; index > to; index--)
                {
                    list[index] = list[index - 1];
                }
            }

            list[to] = tmp;
        }

        public static T[] Clone<T>(this T[] array) where T : class, ICloneable
        {
            T[] clone = new T[array.Length];

            for (int index = 0, count = array.Length; index < count; ++index)
            {
                clone[index] = array[index]?.Clone() as T;
            }

            return clone;
        }

        public static List<T> Clone<T>(this List<T> list) where T : class, ICloneable
        {
            List<T> clone = new List<T>(list.Capacity);

            foreach (T item in list)
            {
                clone.Add(item?.Clone() as T);
            }

            return clone;
        }

        public static Dictionary<int, T> Clone<T>(this Dictionary<int, T> dictionary) where T : class, ICloneable
        {
            Dictionary<int, T> result = new Dictionary<int, T>(dictionary.Count);

            foreach (KeyValuePair<int, T> pair in dictionary)
            {
                result.Add(pair.Key, pair.Value?.Clone() as T);
            }

            return result;
        }

        public static void Resize<T>(this List<T> list, int size, Func<T> value)
        {
            int currentSize = list.Count;
            if (size < currentSize)
            {
                list.RemoveRange(size, currentSize - size);
            }
            else if (size > currentSize)
            {
                if (size > list.Capacity)
                {
                    list.Capacity = size;
                }

                for (int i = 0; i < size - currentSize; i++)
                {
                    list.Add(value.Invoke());
                }
            }
        }

        public static void Resize<T>(this List<T> list, int size) where T : new()
        {
            Resize(list, size, () => new T());
        }

        public static void Dispose<T>(this List<T> list) where T : IDisposable
        {
            for (int i = 0, l = list.Count; i < l; ++i)
            {
                list[i]?.Dispose();
            }

            list.Clear();
        }
    }
}