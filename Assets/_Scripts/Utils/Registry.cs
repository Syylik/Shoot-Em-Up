using System.Collections.Generic;
using UnityEngine;

public delegate T SelectionStrategy<T>(IEnumerable<T> items);
public delegate IEnumerable<T> LargeSelectionStrategy<T>(IEnumerable<T> items);

public static class Registry<T> where T : class
{
    private static HashSet<T> values = new HashSet<T>();

    public static bool TryAdd(T item) => item != null && values.Add(item);

    public static bool Remove(T item) => values.Remove(item);
    public static IEnumerable<T> All() => values;

    public static T Get(SelectionStrategy<T> strategy) => strategy(values);
    public static IEnumerable<T> Get(LargeSelectionStrategy<T> strategy) => strategy(values);
}