using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce composant gère le déplacement d'une entité
/// </summary>
public class EntityMovement : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;

    Vector3 _direction;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction">La direction souhaité</param>
    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    void FixedUpdate()
    {
        var calculatedDirection = (_direction * _speed * Time.fixedDeltaTime);
        _animator.SetFloat("Speed", calculatedDirection.magnitude);

        if (calculatedDirection.magnitude > 0.01f)
        {
            if (calculatedDirection.x > 0)
            {
                _rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                _rb.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        _rb.MovePosition(transform.position + calculatedDirection);
    }

    #region EDITOR
    private void Reset()
    {
        _speed = 5f;
    }
    #endregion

}
