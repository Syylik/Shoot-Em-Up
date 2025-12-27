using UnityEngine;

public class EnableOnMobile : MonoBehaviour
{
    private void Awake() 
    {
        if(Application.isMobilePlatform) gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
