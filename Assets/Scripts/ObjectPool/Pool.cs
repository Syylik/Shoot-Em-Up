using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour, IPoolObject 
{
    private Stack<T> _pool;
    private T _tPrefab;

    private readonly int _maxCount = 12;

    public Pool(T prefab, int capacity = 12)
    {
        _tPrefab = prefab;
        _pool = new Stack<T>(capacity);
        _maxCount = capacity;
    }

    /// <summary>
    /// Get object from pool
    /// </summary>
    /// <param name="position">New position</param>
    /// <param name="rotation">New rotation</param>
    /// <returns>New object to use</returns>
    public T Spawn(Vector2 position, Quaternion rotation)
    {
        if(_pool.Count > 0)
        {
            T newObject = _pool.Pop();
            newObject.Enable(position, rotation);
            return newObject;
        }
        else return Object.Instantiate(_tPrefab, position, rotation);
    }

    /// <summary>
    /// Despawn object and push to pool
    /// </summary>
    /// <param name="obj">object to pool</param>
    public void Despawn(T obj)
    {
        obj.Disable();
        if(_pool.Count < _maxCount) _pool.Push(obj);
        else Object.Destroy(obj.gameObject);
    }
}