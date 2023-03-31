using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement), typeof(Shoot), typeof(Health))]
public class Player : MonoBehaviour
{
    private Movement _playerMove;
    private Shoot _playerShoot;
    private PlayerUI _playerUI;
    private PlayerHealth _playerHealth;

    private Input _input;

    internal Animator _anim;

    private void Awake() => Init();

    private void Init()
    {
        _input = InputSystem.input;
        _playerMove = GetComponent<Movement>();
        _playerShoot = GetComponent<Shoot>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerUI = GetComponent<PlayerUI>();
        _anim = GetComponent<Animator>();

        _playerHealth.OnDie.AddListener(GameControl.Instance.Loose);
    }

    private void Start()
    {
        _input.Game.Fire.performed += context => _playerShoot.ShootState(true);
        _input.Game.Fire.canceled += context => _playerShoot.ShootState(false);
    }

    private void Update()
    {
        _playerMove.moveDirection = _input.Game.Move.ReadValue<Vector2>();
    }
}