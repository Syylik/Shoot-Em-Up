using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    public static Input input { get; private set; }

    public static PlayerInput playerInput { get; private set; }

    public static InputSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        input = new Input();
        playerInput = GetComponent<PlayerInput>();
    }

    public void SwitchInputMap(string mapName) => playerInput.SwitchCurrentActionMap(mapName);

    private void OnEnable() => input.Enable();

    private void OnDisable() => input.Disable();
}
