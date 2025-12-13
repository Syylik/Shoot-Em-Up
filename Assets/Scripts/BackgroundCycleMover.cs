using UnityEngine;

public class BackgroundCycleMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _speedByTime = 0.01f;
    [SerializeField] private float _maxSpeed = 4f;
    [SerializeField] private Transform _background;
    [SerializeField] private float threshold;

    private void Update()
    {
        if(_speed < _maxSpeed) _speed += _speedByTime * Time.deltaTime;
        _background.Translate(Vector2.down * _speed * Time.deltaTime);
        if(_background.transform.position.y < threshold) _background.transform.position = Vector2.zero; 
    }
}