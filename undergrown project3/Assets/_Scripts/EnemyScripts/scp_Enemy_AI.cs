using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class scp_Enemy_AI : MonoBehaviour
{
    [Header("Enemy Settings")]

    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth;
    [SerializeField] private float IdleSpeed = 1f;
    [SerializeField] private float WalkingSpeed = 6f;
    [SerializeField] private float RunningSpeed = 9f;
    [SerializeField] private float RestTime = 3f;

    [Header("References")]

    public scp_Enemy_Manager _EnemyManager;
    public scp_MusicManager _MusicManager;
    public scp_Respawn respawnManager;
    [SerializeField] private GameObject huntManager;
    [SerializeField] private GameObject attackManager;
    [SerializeField] private GameObject Mesh;
    [SerializeField] private GameObject SkeleDeathSound;
    private scp_Enemy_AI_Hit hitManager;
    private scp_Enemy_Sword swordManager;
    private scp_Enemy_Counter enemyCounter;
    private scp_setPlants plantScript;
    private Transform playerTf;
    private Transform myTf;
    [SerializeField] private GameObject[] waypoints;
    private Light healthLight;


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
    public bool _BackUp;
    public bool _PlayerDeadCheck = false;
    public bool _RespawnCheck = false;

    void Start()
    {
        //hitManager._MaxHealth = MaxHealth;
        _EnemyManager = FindObjectOfType<scp_Enemy_Manager>();
        _MusicManager = FindObjectOfType<scp_MusicManager>();
        respawnManager = FindObjectOfType<scp_Respawn>();
        enemyCounter = FindObjectOfType<scp_Enemy_Counter>();
        plantScript = FindObjectOfType<scp_setPlants>();
        agent = GetComponent<NavMeshAgent>();
        capsule = GetComponent<Collider>();
        myTf = GetComponent<Transform>();
        hitManager = GetComponentInChildren<scp_Enemy_AI_Hit>();
        swordManager = GetComponentInChildren<scp_Enemy_Sword>();
        healthLight = GetComponentInChildren<Light>();
        playerTf = _EnemyManager._Player.transform;
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");


        updateDestination();
        updateBehaviour();
    }

    void Update()
    {
        Health = hitManager._CurrentHealth;
        MaxHealth = hitManager._MaxHealth;

        updateBehaviour();

        if (plantScript != null && plantScript.endGame) Destroy(gameObject);

        if (!_Dying)
        {
            if ((_Hunting || _Attacking) && !_PlayerDeadCheck)
            {
                setHuntTarget();
            }

            if (!(_Hunting || _Attacking)) _MusicManager.hunted = false;

            if (_EnemyManager._PlayerDead)
            {
                _Hunting = false;
                if (!_PlayerDeadCheck) _ReturnToPatrol();
            }
            if (!_EnemyManager._PlayerDead && _PlayerDeadCheck) _PlayerDeadCheck = false;

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
        }

        if (respawnManager.respawnCheck) EnemyRespawn();
    }

    private void updateBehaviour()
    {
        if (_Dying && !_Dead)
        {
            WaitToDie();
        }

        if (_Hit)
        {
            agent.enabled = false;
            StartCoroutine(healthLightWait());
            transform.position = hitManager.transform.position;
            _Patrolling = false; _Resting = false; _Hunting = false; _Attacking = false;
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
        int randWaypointIndex = Random.Range(0, waypoints.Length);
        Debug.Log(randWaypointIndex);
        waypointIndex = randWaypointIndex;
    }

    private void updateDestination()
    {
        target = waypoints[waypointIndex].transform.position;
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

    private IEnumerator healthLightWait()
    {
        if (Health <= (MaxHealth / 2))
        {
            float newIntensity = healthLight.intensity * 0.1f;
            yield return new WaitForSeconds(1.5f);
            healthLight.intensity = newIntensity;
        }
    }

    private void WaitToDie()
    {
        Debug.Log(gameObject.name + " died");
        GameObject clone = (GameObject)Instantiate(SkeleDeathSound, transform.position, Quaternion.identity);
        Destroy(clone, 1.0f);
        agent.speed = 0f;
        capsule.enabled = false;
        swordC.enabled = false;
        _Dead = true;
        _EnemyManager.killed++;
        huntManager.SetActive(false); attackManager.SetActive(false);
        _Patrolling = false; _Resting = false; _Hunting = false; _Attacking = false;
        if (enemyCounter != null) enemyCounter.DecreaseCounter();
    }

    public void EnemyRespawn()
    {
        hitManager._CurrentHealth = hitManager._MaxHealth;
        capsule.enabled = true; swordC.enabled = true; agent.enabled = true; healthLight.intensity = 10f;
        attackManager.GetComponent<scp_Enemy_Ai_Attacking>().inAttack = false;
        _Hunting = false; _Attacking = false; _Dying = false; _Dead = false; _EnemyManager._PlayerDead = false; _PlayerDeadCheck = false;
        huntManager.SetActive(true); attackManager.SetActive(true);
        waitAtWaypoint();
    }
}
