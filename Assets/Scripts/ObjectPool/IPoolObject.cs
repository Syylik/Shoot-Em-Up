using UnityEngine;

public interface IPoolObject
{
    /// <summary>
    /// Set active object and nullify all fields 
    /// </summary>
    /// <param name="position">New Object Position</param>
    /// <param name="rotation">New Object Rotation</param>
    public void Enable(Vector2 position, Quaternion rotation);
    
    public void Disable();
}