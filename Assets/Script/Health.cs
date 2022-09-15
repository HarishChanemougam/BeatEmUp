using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Health : MonoBehaviour
{
    [SerializeField] int _startHealth;
    [SerializeField] int _maxHealth;
    [SerializeField] UnityEvent _onDie;
    [SerializeField] HealthBar _healthBar;

    int _currentHealth;

    public event UnityAction OnDamage;
    public event UnityAction OnDie;

    public int CurrentHealth 
    { 
        get
        {
            return _currentHealth;
        }
        // set
        // {
        //     if(value <0)
        //     {
        //         _currentHealth = 0;
        //     }
        //     else if(value > _maxHealth)
        //     {
        //         _currentHealth = _maxHealth;
        //     }
        //     else
        //     {
        //         _currentHealth = value;
        //     }
        // }
    }

    public int MyHPProgress
    {
        get { return CurrentHealth / _maxHealth; }
    }

    public bool IsDead => CurrentHealth <= 0;

    private void Start()
    {
        _currentHealth = _startHealth;
        _healthBar.setMaxHealth(_maxHealth);
    }

    internal void Damage()
    {
        _currentHealth--;    
        OnDamage?.Invoke();

        if (IsDead)
        {
            OnDie.Invoke();
        }

        _healthBar.setHealth(CurrentHealth);
    }
}
