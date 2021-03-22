using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{    
    [HideInInspector]
    public NavMeshAgent navAgent;
    [HideInInspector]
    public PlayerController player;
    public GameController gameController;

    [HideInInspector]
    public Vector3 currentDestination;
    //[HideInInspector]
    public bool aware = false;
    [HideInInspector]
    public float arriveDistance;
    [HideInInspector]
    public int pathDirection = 1;
    //[HideInInspector]
    public int nextPoint;
    public PathPoint nextPathPoint;
    public PathPoint startPathPoint;
    //[HideInInspector]
    public int previousPoint;
    [HideInInspector]
    public AIState currentState;
    //[HideInInspector]
    public float awareness = 0f;
    [HideInInspector]
    public Vector3 playerPosition;
    [HideInInspector]
    public Vector3 previousPlayerPosition;
    [HideInInspector]
    public float ogStoppingDistance;
    [HideInInspector]
    public float ogAlertTime;
    public float searchAlertTime;
    
    [HideInInspector]
    public float fieldOfView = 2f;
    [HideInInspector]
    public Vector3 playerDirection;

    private int frameLimit = 10;
    private int frameIncrement = 0;    
    private int playerMask = 1 << 8;

    [Range(5f, 50f)]
    public float maxViewDistance = 10f;
    [Range(1f, 10f)]
    public float patrolSpeed;
    [Range(1f, 10f)]
    public float chaseSpeedMultiplier = 2f;
    [Range(1f, 45f)]
    public float rotationSpeed = 5f;
    public float alertTime = 3f;    
    public float playerChaseStoppingDistance;

    public Path defaultPath;
    public Path currentPath;

    public List<PathPoint> pathPoints;

    private Transform spherePos;

    private EnemyHearing hearingRange;
    public float hearingLimit = 2f;

    public float timeToLosePlayer = 2f;
    //[HideInInspector]
    public float playerLostTime = 0;
    public float maxAwareness = 5f;

    private Transform eyePos;
    public bool gotPlayer = false;

    void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        navAgent = GetComponent<NavMeshAgent>();
        spherePos = gameObject.transform.Find("Eye");
        eyePos = gameObject.transform.Find("Eyes").transform;
        ogStoppingDistance = navAgent.stoppingDistance;
        ogAlertTime = alertTime;
        hearingRange = transform.GetChild(1).GetComponent<EnemyHearing>();
        navAgent.speed = patrolSpeed;
        nextPoint = 0;
        previousPoint = 0;

        pathPoints = PopulateList(defaultPath);
        SetPath(defaultPath);
        currentState = new PatrolState(this);
        SetState(currentState);
    }

    void Update()
    {
        currentState.UpdateBehavior();
    }

    public void SetState(AIState newState)
    {
        currentState.ExitBehavior();
        currentState = newState;
        currentState.EntryBehavior();
    }

    public void Sight()
    {
        RaycastHit hit, hat;

        if (player && !gotPlayer)
        {
            Vector3 lookTarget = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

            Debug.DrawRay(spherePos.position, transform.forward * maxViewDistance, Color.red, 0.5f);
            if (Physics.SphereCast(spherePos.position, fieldOfView, transform.forward, out hit, maxViewDistance, playerMask))
            {
                Debug.DrawRay(eyePos.position, (player.transform.position - transform.position) * maxViewDistance, Color.green, 0.5f);
                if (Physics.Raycast(eyePos.position, (player.transform.position - transform.position), out hat, maxViewDistance))
                {
                    if (hat.collider.tag == "Player")
                    {
                        aware = true;
                        if (frameIncrement == frameLimit)
                        {
                            previousPlayerPosition = playerPosition;
                            frameIncrement = 0;
                        }
                        frameIncrement++;
                        playerPosition = hat.transform.position;

                        playerDirection = (playerPosition - previousPlayerPosition);

                        //awareness += Time.deltaTime;
                        return;
                    }
                    else
                    {
                        aware = false;
                    }
                }
            }
            else
            {
                aware = false;
            }
            Hearing();
        }
        else
        {
            hearingRange.inRange = false;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            gotPlayer = false;
        }

        
    }

    public void Hearing()
    {
        if (!aware && hearingRange.inRange)
        {
            if (player.noiseLevel > 1)
            {
                playerPosition = player.transform.position;
                aware = true;
            }
            else if (Vector3.Distance(transform.position, player.transform.position) <= hearingLimit)
            {
                playerPosition = player.transform.position;
                aware = true;
            }
        }
    }

    public void Orient(Vector3 target)
    {       
        Vector3 targetPos = new Vector3(target.x, transform.position.y, target.z);
        if (targetPos - transform.position != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(targetPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }        
    }

    public void Teleport(Vector3 targetLocation)
    {
        navAgent.SetDestination(targetLocation);
        navAgent.Warp(targetLocation);
    }

    public List<PathPoint> PopulateList(Path path)
    {
        List<PathPoint> outList = new List<PathPoint>();
        if (path.transform.childCount > 0)
        {
            PathPoint currentPoint = path.gameObject.transform.GetChild(0).GetComponent<PathPoint>();
            while (currentPoint.transform.GetSiblingIndex() != path.transform.childCount - 1)
            {
                outList.Add(currentPoint);
                currentPoint = currentPoint.next;
            }
            outList.Add(currentPoint);
        }
        return outList;
    }

    public void SetPath(Path newPath)
    {
        pathPoints = PopulateList(newPath);
        nextPoint = 0;
        previousPoint = 0;
        pathDirection = 1;
        startPathPoint = pathPoints[0];
        nextPathPoint = pathPoints[0];

        navAgent.SetDestination(nextPathPoint.transform.position);
    }

}
