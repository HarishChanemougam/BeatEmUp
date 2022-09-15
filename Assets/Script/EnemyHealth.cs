using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _startHealth;
    [SerializeField] int _maxHealth;
    [SerializeField] UnityEvent _onDie;
    

    public event UnityAction OnDamage;
    public event UnityAction OnDie;


    [ShowNativeProperty] public int CurrentHealth { get; private set; }

    public int MyHPProgress
    {
        get { return CurrentHealth / _maxHealth; }
    }

    public bool IsDead => CurrentHealth <= 0;

    private void Start()
    {
        CurrentHealth = _startHealth;
        
    }

    internal void Damage()
    {
        CurrentHealth--;  
        OnDamage?.Invoke();

        if (IsDead)
        {
            OnDie?.Invoke();
        }

    }
}
