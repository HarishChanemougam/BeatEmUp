using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class RedBadGuys : MonoBehaviour
{
    [SerializeField] Rigidbody2D _root;
    [SerializeField] float _walkSpeed;
    [SerializeField] float _attackDistance;
    [SerializeField] Animator _animator;
    [SerializeField] AttackHitBox _hitbox;

    public bool IsWalking => _appliedVector.magnitude > 0.01f;
    public float WalkDistance => _appliedVector.magnitude;

    Vector2 _directionAsked;
    Vector2 _appliedVector;
    Vector3 _direction;
    bool _isBlocking;
    bool _isAttack;

    PlayerTag _target;
    bool _attack = true;

    IEnumerator AttackRoutine()
    {
        _attack = false;
        yield return new WaitForSeconds(1f);
        _attack = true;
    }

    void FixedUpdate()
    {
        _animator.SetBool("IsMoving", _direction.magnitude > 0.1f);
        _animator.SetBool("IsBlocking", _isBlocking);

        // Manage movement
        Vector3 direction = Vector3.zero;
        float distanceToPlayer = 10000f;
        if (_target != null)
        {
            distanceToPlayer = Vector3.Distance(_target.transform.position, transform.position);

            direction = _target.transform.position - transform.position;
            direction.Normalize();
        }
        _appliedVector = direction * Time.fixedDeltaTime * _walkSpeed;

        if(distanceToPlayer < _attackDistance && _attack)
        {
            _animator.SetTrigger("isAttack");
            _hitbox.AttackAllCharacters();
            StartCoroutine(AttackRoutine());
        }
        else
        {
            _root.MovePosition(_root.position + _appliedVector);    
        }

        // Rotation
        if (_appliedVector.magnitude < 0.01f)
        {
            // Nothing
        }
        else if (_appliedVector.x < 0)
        {
            _root.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            _root.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Manage attack



    }

    public void SetTarget(PlayerTag player)
    {
        _target = player;
    }

    internal void ClearTarget()
    {
        _target = null;
    }

    /*  #region Editor
  #if UNITY_EDITOR
      void Reset()
      {
          _root = GetComponentInParent<Rigidbody2D>();
          _walkSpeed = 5;
      }
  #endif
      #endregion
  */
}
