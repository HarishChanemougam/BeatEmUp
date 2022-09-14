using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField] EntityMovement _movement;
    [SerializeField] EntityAttack _attack;
    [SerializeField] float _limitDistance;

    PlayerTag _target;

    public void SetTarget(PlayerTag player)
    {
        _target = player;
    }
    internal void ClearTarget()
    {
        _target = null;
    }

    /// <summary>
    /// Prendre les decisions de l'ennemi
    /// </summary>
    void Update()
    {
        if (_target != null) // Si on a detecté le joueur
        {
            // On estime notre distance avec le joueur
            var distanceToPlayer = Vector2.Distance(_target.transform.position, transform.position);
            if (distanceToPlayer < _limitDistance)// Joueur trop proche
            {
                _movement.SetDirection(Vector2.zero);
                _attack.LaunchAttack();
            }
            else
            {
                _movement.SetDirection(_target.transform.position - transform.position);
            }
        }
        else   // Pas de target en cours : on ne bouge pas
        {
            _movement.SetDirection(Vector2.zero);
        }
    }

    #region EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _limitDistance);
    }

    private void Reset()
    {
        _limitDistance = 0.2f;
    }
    #endregion

}
