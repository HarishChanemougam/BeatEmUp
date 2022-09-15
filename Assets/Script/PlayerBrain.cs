using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Gère les inputs du joueurs et qui redistribue les valeurs des inputs aux composants correspondant
/// </summary>
public class PlayerBrain : MonoBehaviour
{
    [Header("Input")]
    //[SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _attackInput;

    [Header("Actions")]
    //[SerializeField] PlayerMovementJordan _movement;
    [SerializeField] EntityAttack _attack;

    void Start()
    {
        _attackInput.action.started += Attack;
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        _attack.LaunchAttack();
    }

    //void Move(InputAction.CallbackContext obj)
    //{
    //    _movement.SetDirection(obj.ReadValue<Vector2>());
    //}
    //
    //void StopMove(InputAction.CallbackContext obj)
    //{
    //    _movement.SetDirection(Vector2.zero);
    //}
}
