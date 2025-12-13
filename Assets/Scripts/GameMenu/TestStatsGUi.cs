using UnityEngine;
using TMPro;

public class TestStatsGUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _fpsCounter;
    private float timeToNextUpdate = 0.0f;
    
    private void Update()
    {
        if(_fpsCounter != null && Time.time >= timeToNextUpdate)
        {
            float fps = 1.0f / Time.deltaTime;
            _fpsCounter.text = "FPS: " + (int)fps;
            timeToNextUpdate = Time.time + 1.0f;
        }
    }
}
