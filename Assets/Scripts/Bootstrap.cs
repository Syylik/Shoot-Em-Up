using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameControl _gameControl;
    [SerializeField] private InputSystem _input;

    private void Awake()
    {
        // _input.Init();
        // _gameControl.Init();

        // _player.Init();
    }
}
