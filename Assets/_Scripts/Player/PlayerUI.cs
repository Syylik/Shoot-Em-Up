using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    private PlayerHealth _health;

    private void Awake() => _health = GetComponent<PlayerHealth>();

    public void UpdateHealth(float health, float maxHealth)
    {
        _healthBar.fillAmount = health / maxHealth;
    }
}
