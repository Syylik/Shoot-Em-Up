using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private TMP_Text _healthAmount;
    private PlayerHealth _health;

    private void Awake() => _health = GetComponent<PlayerHealth>();

    public void UpdateHealth(float health, float maxHealth)
    {
        _healthBar.fillAmount = health / maxHealth;
        _healthAmount.text = $"{(uint)health}/{(uint)maxHealth}";
    }
}
