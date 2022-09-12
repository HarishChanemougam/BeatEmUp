using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputSettings;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] Transform _root;
    [SerializeField] Animator _animator;
    [SerializeField] float _movingThreshold;
    [SerializeField] float _speed;

    Vector2 _playerMovement;

#if UNITY_EDITOR
    private void Reset()
    {
        _speed = 5f;

    }
#endif

    private void Start()
    {
        _moveInput.action.started += StartMove;
        _moveInput.action.performed += UpdateMove;
        _moveInput.action.canceled += EndMove;
    }

    private void Update() 
    {
        Vector2 direction = new Vector2(_playerMovement.x, 0);
        _root.transform.Translate(_playerMovement * Time.deltaTime * _speed, Space.World);
        
        if (direction.magnitude > _movingThreshold) 
        {
            _animator.SetBool("IsWalking", true);

        }

        else
        {
            _animator.SetBool("IsWalking", false); 
        }
    }

    private void StartMove(InputAction.CallbackContext obj)
    {
        _playerMovement = obj.ReadValue<Vector2>();

        

    }

    private void UpdateMove(InputAction.CallbackContext obj)
    {
        _playerMovement = obj.ReadValue<Vector2>();
       
    }

    private void EndMove(InputAction.CallbackContext obj)
    {
        _playerMovement = new Vector2(0, 0);
        
    }
}
