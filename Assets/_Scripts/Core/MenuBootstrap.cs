using UnityEngine;

public class MenuBootstrap : MonoBehaviour
{
    [SerializeField] private InputSystem _input;

    private void Awake()
    {
        _input.Init();
    }
}
