using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    [Header("Conf")]
    [SerializeField] Transform _root;
    [SerializeField] Transform _targetedPlayer;

    [Header("Actions")]
    [SerializeField] EntityMovement _movement;
    [SerializeField] Attack _attack;

    [Header("Conf")]
    [SerializeField] float _attackThreshold;
    [SerializeField] float _moveThreshold;
    [SerializeField] float _attackCooldown;

   /* float _attackCooldownRuntime;

    private void Start()
    {
        _attackCooldownRuntime = 0f;
    }
*/
    /*private void Update()
    {
        _attackCooldownRuntime -= Time.deltaTime;

        float distance = Vector3.Distance(_targetedPlayer.position, transform.position);
        if (distance < _attackThreshold)    // Le joueur est dans la zone bleue
        {
            if (_attackCooldownRuntime <= 0)  // Le sablier a atteind sa durée limite
            {
                _attack.LaunchAttack();
                _attackCooldownRuntime = _attackCooldown;   // On rempli le sablier
            }
            _movement.SetDirection(Vector2.zero);
        }
        else if (distance < _moveThreshold)  // Le joueur est dans la zone jaune
        {
            _movement.SetDirection(_targetedPlayer.position - transform.position);
        }
        else     // Le joueur est hors de vue
        {
            _movement.SetDirection(Vector2.zero);
        }
    }*/

  /*  #region Editor
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attackThreshold);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _moveThreshold);
    }

    private void Reset()
    {
        _root = transform.parent;
        _movement = _root.GetComponentInChildren<Movement>();
        _attack = _root.GetComponentInChildren<Attack>();
    }
#endif
    #endregion
*/
}
