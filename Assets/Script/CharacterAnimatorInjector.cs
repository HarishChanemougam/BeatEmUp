using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorInjector : MonoBehaviour
{
    [SerializeField] Transform _root;
    [SerializeField] Animator _animator;

    [Header("Dependencies")]
    [SerializeField] Movement _movement;
    [SerializeField] Attack _attack;
    [SerializeField] Health _health;

    [Header("Parameters")]
    [SerializeField] string _attackTrigger;
    [SerializeField] string _damageTrigger;
    [SerializeField] string _onDeadTrigger;
    [SerializeField] string _isWalkingBool;

    private void Start()
    {
        _attack.OnAttack += AttackEvent;
        _health.OnDamage += DamageEvent;
        _health.OnDie += DieEvent;

    }

    private void OnDestroy()
    {
        _attack.OnAttack -= AttackEvent;
        _health.OnDamage -= DamageEvent;
        _health.OnDie -= DieEvent;
    }

    void DieEvent() => _animator.SetTrigger(_onDeadTrigger);
    void AttackEvent() => _animator.SetTrigger(_attackTrigger);
    void DamageEvent() => _animator.SetTrigger(_damageTrigger);

    void LateUpdate()
    {
        _animator.SetBool(_isWalkingBool, _movement.IsWalking);
    }

    #region EDITOR
#if UNITY_EDITOR
    private void Reset()
    {
        // Setup basic root
        _root = transform.parent;
        _animator = GetComponent<Animator>();

        // Try find components
        _movement = _root.GetComponentInChildren<Movement>();
        _attack = _root.GetComponentInChildren<Attack>();
        _health = _root.GetComponentInChildren<Health>();

        // Reset params
        _attackTrigger = "OnAttack";
        _damageTrigger = "OnDamage";
        _onDeadTrigger = "OnDead";
        _isWalkingBool = "IsWalking";
    }
#endif
    #endregion

}
