using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedBadGuys : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] PlayerMovementJordan _player;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;



    void Start()
    {
        _player = FindObjectOfType<PlayerMovementJordan>();

    }

    private void FixedUpdate()
    {
        _rb.velocity = (transform.forward * _moveSpeed);
    }

    void Update()
    {
        transform.LookAt(_player.transform.position);
    }

}
