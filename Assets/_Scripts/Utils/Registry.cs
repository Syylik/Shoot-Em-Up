using System.Collections.Generic;
using UnityEngine;

public delegate T SelectionStrategy<T>(IEnumerable<T> items);
public delegate IEnumerable<T> LargeSelectionStrategy<T>(IEnumerable<T> items);

public static class Registry<T> where T : class
{
    private static HashSet<T> _items = new HashSet<T>();
    public static IReadOnlyCollection<T> Items => _items;

    public static bool TryAdd(T item) => item != null && _items.Add(item);
    public static bool Remove(T item) => _items.Remove(item);

    public static IEnumerable<T> All() => _items;

    public static int Count => _items.Count;
    
    public static T Get(SelectionStrategy<T> strategy) => strategy(_items);
    public static IEnumerable<T> Get(LargeSelectionStrategy<T> strategy) => strategy(_items);
}