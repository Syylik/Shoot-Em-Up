using UnityEngine;

public class EnableOnMobile : MonoBehaviour
{
    private void Awake() 
    {
#if UNITY_ANDROID && !UNITY_EDITOR     
        gameObject.SetActive(true);
#else
        gameObject.SetActive(false);
#endif
    
    }
}
