using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
};

public class RedBadGuys : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] float range = 2f;
    [SerializeField] float moveSpeed = 2f;

    EnemyState currState = EnemyState.Wander;

    // Update is called once per frame
    void Update()
    {
        // State
        switch (currState)
        {
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                // Die();
                break;
        }

        // À porté
        if (IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        // Trop loin
        else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, target.position) <= range;
    }

    bool isFacingRight()
    {
        return transform.rotation.eulerAngles.y > 0;
    }

    void Wander()
    {
        myRigidbody.velocity = Vector2.zero;
        return;

        if (isFacingRight())
        {
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    //}

    void Follow()
    {
        if (transform.position.x > target.position.x)
        {
            //target is left
            transform.localScale = new Vector2(-1, 1);
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), moveSpeed * Time.deltaTime);
        }
        else if (transform.position.x < target.position.x)
        {
            //target is right
            transform.localScale = new Vector2(1, 1);
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), moveSpeed * Time.deltaTime);
        }
    }
}
