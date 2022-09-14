using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
    [SerializeField] AttackHitBox _hitBox;

    public event UnityAction OnAttack;

    internal void LaunchAttack()
    {
        foreach (var el in _hitBox.DetectedHealths)
        {
            el.Damage();
        }

        OnAttack?.Invoke();
    }



    #region EDITOR
#if UNITY_EDITOR
    private void Reset()
    {
        _hitBox = transform.parent.GetComponentInChildren<AttackHitBox>();
    }
#endif
    #endregion

}
