using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBadGuys : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] PlayerMovement _player;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerMovement>();
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
