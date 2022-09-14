using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody2D _root;
    [SerializeField] float _walkSpeed;

    public bool IsWalking => _appliedVector.magnitude > 0.01f;
    public float WalkDistance => _appliedVector.magnitude;

    Vector2 _directionAsked;
    Vector2 _appliedVector;

    void FixedUpdate()
    {
        _appliedVector = _directionAsked.normalized * Time.fixedDeltaTime * _walkSpeed;
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
        _root.MovePosition(_root.position + _appliedVector);
    }

    public void SetDirection(Vector2 vector2)
    {
        _directionAsked = vector2;
    }

    #region Editor
#if UNITY_EDITOR
    void Reset()
    {
        _root = GetComponentInParent<Rigidbody2D>();
        _walkSpeed = 5;
    }
#endif
    #endregion

}
