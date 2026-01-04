using System.Collections.Generic;
using UnityEngine;

public delegate T SelectionStrategy<T>(IEnumerable<T> items);
public delegate IEnumerable<T> LargeSelectionStrategy<T>(IEnumerable<T> items);

public static class Registry<T> where T : class
{
    private static HashSet<T> items = new HashSet<T>();

    public static bool TryAdd(T item) => item != null && items.Add(item);
    public static bool Remove(T item) => items.Remove(item);

    public static IEnumerable<T> All() => items;

    public static int Count => items.Count;
    
    public static T Get(SelectionStrategy<T> strategy) => strategy(items);
    public static IEnumerable<T> Get(LargeSelectionStrategy<T> strategy) => strategy(items);
}