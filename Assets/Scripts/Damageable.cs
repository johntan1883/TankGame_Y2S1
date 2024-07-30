using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent OnDead;
    public UnityEvent <float> OnHealthChange;
    public UnityEvent OnHit, OnHeal;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    public int CurrentHealth //Property for current health
    {
        get { return currentHealth; }
        set 
        {
            currentHealth = value;
            OnHealthChange?.Invoke((float)currentHealth / maxHealth);
        }
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    internal void Hit(int damagePoints)
    {
        CurrentHealth -= damagePoints;

        if (CurrentHealth < 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    }

    public void Heal(int healthBoost)
    {
        CurrentHealth += healthBoost;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        OnHeal?.Invoke();
    }
}
