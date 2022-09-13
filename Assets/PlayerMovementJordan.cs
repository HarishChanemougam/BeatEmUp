using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputSettings;

public class PlayerMovementJordan : MonoBehaviour
{

    [SerializeField] InputActionReference _move;
    [SerializeField] InputActionReference _sprint;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;

    [SerializeField] float _speed;

    Vector3 _direction;
    Vector3 _aimDirection;
    bool _isRunning;

    private void Start()
    {
        _move.action.started += StartMove;
        _move.action.performed += UpdateMove;
        _move.action.canceled += StopMove;

        _sprint.action.started += StartSprint;
        _sprint.action.canceled += StopSprint;
    }

    private void StopSprint(InputAction.CallbackContext obj)
    {
        _isRunning = false;
    }

    private void StartSprint(InputAction.CallbackContext obj)
    {
        _isRunning = true;
    }

    private void FixedUpdate()
    {
        Debug.Log($"{_direction}");

        
        _animator.SetBool("IsMoving", _direction.magnitude > 0.1f);
        _animator.SetBool("IsRunning", _isRunning);

        _rb.MovePosition(_rb.transform.position + (_direction * Time.fixedDeltaTime * _speed));


        if (_isRunning)
        {
            _rb.MovePosition(_rb.transform.position + (_direction * Time.fixedDeltaTime * _speed * 2));
        }
        else
        {
            _rb.MovePosition(_rb.transform.position + (_direction * Time.fixedDeltaTime * _speed));
        }

    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        _direction = Vector3.zero;
    }

    private void UpdateMove(InputAction.CallbackContext obj)
    {
        _direction = obj.ReadValue<Vector2>();
        _aimDirection = _direction;
    }

    private void StartMove(InputAction.CallbackContext obj)
    {
        _direction = obj.ReadValue<Vector2>();
        _aimDirection = _direction;
    }

}
