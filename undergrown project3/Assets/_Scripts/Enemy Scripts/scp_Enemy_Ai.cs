using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class scp_Enemy_AI : MonoBehaviour
{
    [Header("Enemy Settings")]

    [SerializeField] private float IdleSpeed = 1f;
    [SerializeField] private float WalkingSpeed = 6f;
    [SerializeField] private float RunningSpeed = 9f;
    [SerializeField] private float RestTime = 3f;

    [Header("References")]

    public scp_Enemy_Manager _EnemyManager;
    private Transform playerTf;
    private Transform[] waypoints;

    private NavMeshAgent agent;

    private int waypointIndex;
    private Vector3 target;

    [HideInInspector] public bool _Patrolling = true;
    [HideInInspector] public bool _Hunting;
    [HideInInspector] public bool _Resting;
    [HideInInspector] public bool _Attacking;
    [HideInInspector] public bool _PlayerDeadCheck = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTf = _EnemyManager._Player.transform;
        waypoints = _EnemyManager._Waypoints;
        
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

        if ((_Hunting || _Attacking) && !_PlayerDeadCheck)
        {
            setHuntTarget();
        }

        if (_EnemyManager._PlayerDead && !_PlayerDeadCheck)
        {
            _ReturnToPatrol();
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

        if (_Hunting && !_PlayerDeadCheck)
        {
            agent.enabled = true;
            agent.speed = RunningSpeed;
            _Patrolling = false; _Resting = false;
        }

        if (_Attacking)
        {
            agent.speed = IdleSpeed;
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
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    private IEnumerator waitAtWaypoint()
    {
        _Patrolling = false; _Resting = true;

        yield return new WaitForSeconds(RestTime);

        _Resting = false; _Patrolling = true;
    }

    private void setHuntTarget()
    {
        target = playerTf.position;
        agent.SetDestination(target);
    }

    public void _ReturnToPatrol()
    {
        _PlayerDeadCheck = true;
        _Hunting = false;
        _Attacking = false;
        if (_Resting) return;
        else _Patrolling = true;

        //Debug.Log(gameObject + "knows player is dead");
    }
}
