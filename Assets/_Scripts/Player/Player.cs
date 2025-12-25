using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement), typeof(Shoot), typeof(Health))]
public class Player : MonoBehaviour
{
    private Movement _playerMove;
    private Shoot _playerShoot;
    private PlayerHealth _playerHealth;

    private Input _input;

    
    public void Init(Input input)
    {
        _input = input;
        _playerMove = GetComponent<Movement>();
        _playerShoot = GetComponent<Shoot>();
        _playerHealth = GetComponent<PlayerHealth>();
        
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