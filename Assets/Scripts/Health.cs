using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;

    [SerializeField] internal GameObject dieEffect;

    [SerializeField] internal RandomSound damageSound;
    [SerializeField] internal RandomSound dieSound;

    public UnityEvent<float, float> OnHealthChanged;
    public UnityEvent OnDie;

    internal bool canTakeHit = true;

    private void Start() => health = maxHealth;

    public virtual void ChangeHealth(float value)
    {
        if(!canTakeHit) return;
        health += value;
        health = Mathf.Clamp(health, 0, maxHealth);
        OnHealthChanged?.Invoke(health, maxHealth);
        if(health == 0)
        {
            Die();
            return;
        }
        damageSound.Play();
    }

    public virtual void Die()
    {
        OnDie?.Invoke();
        dieSound.Play();
        Destroy(Instantiate(dieEffect, transform.position, Quaternion.identity), 1.5f);
    }

    public void TakeHitState(bool state) => canTakeHit = state;
}
