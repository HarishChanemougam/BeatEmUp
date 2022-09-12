using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RedBadGuys : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] PlayerMovement _player;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;

    private float _startWaitTime = 4;
    private float _timeToRotate = 2;
    private float _walkSpeed = 5;
    private float _runSpeed = 7;
    private float _viewRadius = 15;
    private float _viewAngle = 90;
    private LayerMask _playerMask;
    private LayerMask _obstacleMask;
    private float _meshResolution = 1f;
    private int _edgeIterations = 4;
    private float _edgeDistance = 0.5f;

    private Transform[] waypoints;
    int m_currentWaypointIndex;
    private NavMeshAgent navMeshAgent;
    Vector2 playerLastPosition = Vector2.zero;
    Vector2 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_playerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;


    void Start()
    {
        /*_player = FindObjectOfType<PlayerMovement>();*/
        m_PlayerPosition = Vector2.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_playerInRange = false;
        m_WaitTime = _startWaitTime;
        m_TimeToRotate = _timeToRotate;

        m_currentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = _walkSpeed;
        navMeshAgent.SetDestination(waypoints[m_currentWaypointIndex].position);
    }

    /* private void FixedUpdate()
     {
         _rb.velocity = (transform.forward * _moveSpeed);
     }

     void Update()
     {
         transform.LookAt(_player.transform.position);
     }*/

    private void Update()
    {
        EnviromentView();

        if (!m_IsPatrol)
        {
            Chasing();
        }

        else
        {
            Patroling();
        }
    }
    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector2.zero;

        if (!m_CaughtPlayer)
        {
            Move(_runSpeed);
            navMeshAgent.SetDestination(m_PlayerPosition);
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            /*if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector2.Distance(transform.position, GameObject.FindGameObjectsWithTag("Player")transform.position) >= 6f)
            {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(_walkSpeed);
                m_TimeToRotate = _timeToRotate;
                m_WaitTime = _startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_currentWaypointIndex].position);
            }

            else
            {
                if (Vector2.Distance(transform.position, GameObject.FindGameObjectsWithTag("Player").transform.position) >= 2.5f)
                    Stop();
                m_WaitTime -= Time.deltaTime;
            }*/
        }
    }
    private void Patroling()
    {
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(_walkSpeed);
                LookingPlayer(playerLastPosition);
            }

            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }

        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector2.zero;
            navMeshAgent.SetDestination(waypoints[m_currentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_WaitTime <= 0)

                {
                    NextPoint();
                    Move(_walkSpeed);
                    m_WaitTime = _startWaitTime;
                }

                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }


    private void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    private void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    private void NextPoint()
    {
        m_currentWaypointIndex = (m_currentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_currentWaypointIndex].position);
    }

    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }

    void LookingPlayer(Vector2 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector2.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(_walkSpeed);
                navMeshAgent.SetDestination(waypoints[m_currentWaypointIndex].position);
                m_WaitTime = _startWaitTime;
                m_TimeToRotate = _timeToRotate;

            }

            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(position: transform.position, _viewRadius, _playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector2 dirToPlayer = (player.position - transform.position).normalized;

            if (Vector2.Angle(transform.forward, dirToPlayer) < _viewAngle / 2)
            {
                float dstTOPlayer = Vector2.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstTOPlayer, _obstacleMask))
                {
                    m_playerInRange = true;
                    m_IsPatrol = false;
                }

                else
                {
                    m_playerInRange = false;
                }
            }

            if (Vector2.Distance(transform.position, player.position) > _viewRadius)
            {
                m_playerInRange = false;
            }

            if (m_playerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }

    }
}
