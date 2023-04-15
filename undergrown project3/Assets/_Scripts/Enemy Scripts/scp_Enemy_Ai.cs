using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.AI;

public class scp_Enemy_Ai : MonoBehaviour
{
    [Header("Enemy Variables")]

    [SerializeField] private float IdleSpeed = 1f;
    [SerializeField] private float WalkingSpeed = 6f;
    [SerializeField] private float RunningSpeed = 9f;

    [Header("References")]

    [SerializeField] private Transform[] Waypoints;
    [SerializeField] private Transform playerTf;

    private NavMeshAgent agent;

    private int waypointIndex;
    private Vector3 target;

    [HideInInspector] public bool _Patrolling = true;
    [HideInInspector] public bool _Hunting;
    [HideInInspector] public bool _Resting;
    [HideInInspector] public bool _Attacking;
    private float restTime = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        updateDestination();
        updateBehaviour();
    }

    void Update()
    {
        updateBehaviour();

        if (_Patrolling)
        {
            bool inWaypointProx = false;
            if (Vector3.Distance(transform.position, target) < 3) inWaypointProx = true;
            else inWaypointProx = false;

            if (inWaypointProx)
            {
                //Debug.Log("Reached waypoint");
                iterateWaypointIndex();
                updateDestination();
                StartCoroutine(waitAtWaypoint());
            }
        }

        if (_Hunting || _Attacking)
        {
            setHuntTarget();
        }
    }

    private void updateBehaviour()
    {
        if (_Patrolling)
        {
            agent.enabled = true;
            updateDestination();
            agent.speed = WalkingSpeed;
        }

        if (_Resting)
        {
            agent.enabled = false;
            agent.speed = IdleSpeed;
        }

        if (_Hunting)
        {
            agent.enabled = true;
            agent.speed = RunningSpeed;
            _Patrolling = false; _Resting = false;
        }

        if (_Attacking)
        {
            agent.enabled = true;
            agent.speed = RunningSpeed;
            _Patrolling = false; _Resting = false; _Hunting = false;
        }
    }

    private void iterateWaypointIndex()
    {
        int randWaypointIndex = Random.Range(0, 6);
        waypointIndex = randWaypointIndex;
    }

    private void updateDestination()
    {
        target = Waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    private IEnumerator waitAtWaypoint()
    {
        _Patrolling = false; _Resting = true;

        yield return new WaitForSeconds(restTime);

        _Resting = false; _Patrolling = true;
    }

    private void setHuntTarget()
    {
        target = playerTf.position;
        agent.SetDestination(target);
    }
}
