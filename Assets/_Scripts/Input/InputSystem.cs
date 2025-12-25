using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    public Input input { get; private set; }

    public static PlayerInput playerInput { get; private set; }

    public static InputSystem Instance { get; private set; }

    public void Init()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        input = new Input();
        input.Enable();
        playerInput = GetComponent<PlayerInput>();
    }

    public void SwitchInputMap(string mapName) => playerInput.SwitchCurrentActionMap(mapName);

    private void OnDisable() => input.Disable();

    private void OnDestroy() { if(Instance == this) Instance = null; }
}
