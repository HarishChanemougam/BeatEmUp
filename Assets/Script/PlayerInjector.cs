using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInjector : MonoBehaviour
{
    [SerializeField] Transform _root;

    [Header("Input")]
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _attackInput;

    [Header("Dependencies")]
    [SerializeField] Movement _movement;
    [SerializeField] Attack _attack;

    void Start()
    {
        // Walk
        _moveInput.action.started += Walk;
        _moveInput.action.performed += Walk;
        _moveInput.action.canceled += StopWalk;

        // Attack
        _attackInput.action.started += LaunchAttack;
    }

    void OnDestroy()
    {
        // Walk
        _moveInput.action.started -= Walk;
        _moveInput.action.performed -= Walk;
        _moveInput.action.canceled -= StopWalk;

        // Attack
        _attackInput.action.started -= LaunchAttack;
    }

    void LaunchAttack(InputAction.CallbackContext obj)
    {
        _attack.LaunchAttack();
    }
    void Walk(InputAction.CallbackContext obj)
    {
        _movement.SetDirection(obj.ReadValue<Vector2>());
    }
    void StopWalk(InputAction.CallbackContext obj)
    {
        _movement.SetDirection(Vector2.zero);
    }

    #region EDITOR
#if UNITY_EDITOR
    private void Reset()
    {
        _root = transform.parent;
        _movement = _root.GetComponentInChildren<Movement>();
        _attack = _root.GetComponentInChildren<Attack>();
    }
#endif
    #endregion

}
