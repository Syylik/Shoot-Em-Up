using UnityEngine;

[RequireComponent(typeof(Movement), typeof(Shoot), typeof(Health))]
public class Player : MonoBehaviour
{
    private Movement _playerMove;
    private Shoot _playerShoot;
    private PlayerUI _playerUI;
    private PlayerHealth _playerHealth;
    private PlayerInput _input;

    internal Animator _anim;

    private void Awake()
    {
        _input = new PlayerInput();
        _playerMove = GetComponent<Movement>();
        _playerShoot = GetComponent<Shoot>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerUI = GetComponent<PlayerUI>();
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void Start()
    {
        _input.Main.Fire.performed += context => _playerShoot.ShootState(true);
        _input.Main.Fire.canceled += context => _playerShoot.ShootState(false);
    }

    private void Update()
    {
        _playerMove.Move(_input.Main.Move.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        _input.Disable();
    }
}