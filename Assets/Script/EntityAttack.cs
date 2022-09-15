using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public void LaunchAttack()
    {
        Debug.Log($"{transform.parent.name} : OH MY GOD");

        _animator.SetTrigger("isAttack");

    }


}
