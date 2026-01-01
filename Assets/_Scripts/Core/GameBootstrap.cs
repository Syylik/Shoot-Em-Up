using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameControl _gameControl;
    [SerializeField] private WaveControl _waveControl;
    [SerializeField] private EnemyCreator _enemyCreator;
    [SerializeField] private InputSystem _input;

    private void Awake()
    {
        _input.Init();
        _gameControl.Init();

        _player.Init(_input.input);
    }

    private void Start()
    {
        _waveControl.StartWaves(_enemyCreator);
    }
}