using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour, IPoolObject 
{
    private Stack<T> _pool;
    private T _tPrefab;

    public Pool(T prefab, int capacity = 12)
    {
        _tPrefab = prefab;
        _pool = new Stack<T>(capacity);
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

    public void Despawn(T obj)
    {
        obj.Disable();
        _pool.Push(obj);
    }
}