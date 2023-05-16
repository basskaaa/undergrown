using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class scp_Enemy_AI : MonoBehaviour
{
    [Header("Enemy Settings")]

    [SerializeField] private int Health;
    [SerializeField] private float IdleSpeed = 1f;
    [SerializeField] private float WalkingSpeed = 6f;
    [SerializeField] private float RunningSpeed = 9f;
    [SerializeField] private float RestTime = 3f;

    [Header("References")]

    public scp_Enemy_Manager _EnemyManager;
    [SerializeField] private GameObject huntManager;
    [SerializeField] private GameObject attackManager;
    private scp_Enemy_AI_Hit hitManager;
    private Transform playerTf;
    private Transform myTf;
    private Transform[] waypoints;


    private NavMeshAgent agent;
    private Collider capsule;
    [SerializeField] private Collider swordC;

    private int waypointIndex;
    private Vector3 target;

    public bool _Patrolling = true;
    public bool _Hunting;
    public bool _Resting;
    public bool _Attacking;
    public bool _Hit;
    public bool _Dying;
    public bool _Dead;
    public bool _PlayerDeadCheck = false;

    void Start()
    {
        _EnemyManager = FindObjectOfType<scp_Enemy_Manager>();
        agent = GetComponent<NavMeshAgent>();
        capsule = GetComponent<Collider>();
        myTf = GetComponent<Transform>();
        hitManager = GetComponentInChildren<scp_Enemy_AI_Hit>();
        playerTf = _EnemyManager._Player.transform;
        waypoints = _EnemyManager._Waypoints;
        
        updateDestination();
        updateBehaviour();
    }

    void Update()
    {
        transform.position = hitManager.transform.position;

        Health = hitManager._CurrentHealth;

        updateBehaviour();

        if (!_Dying)
        {
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
    }

    private void updateBehaviour()
    {
        if (_Dying)
        {
            if (!_Dead) Debug.Log(gameObject.name + " died");
            agent.speed = 0f;
            capsule.enabled = false;
            swordC.enabled = false;
            _Dead = true;
            huntManager.SetActive(false); attackManager.SetActive(false);
            _Patrolling = false; _Resting = false; _Hunting = false; _Attacking = false;
            Destroy(gameObject, 30.0f);
        }

        if (_Hit)
        {
            //agent.speed = IdleSpeed;
            _Patrolling = false; _Resting = false; _Hunting = false; _Attacking = false;
            agent.velocity = target * 8;
        }

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

        if (_Hunting && !_PlayerDeadCheck && !_Hit)
        {
            agent.enabled = true;
            agent.speed = RunningSpeed;
            _Patrolling = false; _Resting = false;
        }

        if (_Attacking && !_Hit)
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
    }
}
