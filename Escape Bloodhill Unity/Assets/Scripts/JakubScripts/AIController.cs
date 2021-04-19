using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    /*
    [HideInInspector]
    public Vector3 currentDestination;
    [HideInInspector]
    public int nextPoint;
    [HideInInspector]
    public int previousPoint;
    public Path defaultPath;
    */

    [HideInInspector]
    public NavMeshAgent navAgent;
    [HideInInspector]
    public PlayerController player;
    //[HideInInspector]
    public Vector3 playerPosition;
    [HideInInspector]
    public Vector3 previousPlayerPosition;
    [HideInInspector]
    public Vector3 playerDirection;
    [HideInInspector]
    public GameController gameController;
    [HideInInspector]
    public AIState currentState;
    //[HideInInspector]
    public EyeLights eyeGlower;

    //[HideInInspector]
    public bool aware = false;
    //[HideInInspector]
    public float awareness = 0f;
    public float maxAwareness = 5f;
    [HideInInspector]
    public float ogAlertTime;
    public float alertTime;
    public float searchAlertTime;

    public Transform eyePos;
    private Transform spherePos;
    [HideInInspector]
    public float fieldOfView = 3f;
    private EnemyHearing hearingRange;
    public float hearingLimit = 2f;
    [Range(5f, 50f)]
    public float maxViewDistance = 10f;

    [Range(1f, 45f)]
    public float rotationSpeed = 5f;
    [Range(1f, 10f)]
    public float patrolSpeed;
    [Range(1f, 10f)]
    public float chaseSpeedMultiplier = 2f;
    [HideInInspector]
    public float arriveDistance;
    [HideInInspector]
    public float ogStoppingDistance;
    public float playerChaseStoppingDistance;
    public float timeToLosePlayer = 2f;
    [HideInInspector]
    public float playerLostTime = 0;
    [HideInInspector]
    public bool gotPlayer = false;

    [HideInInspector]
    public int pathDirection = 1;
    //[HideInInspector]
    public PathPoint currentPathPoint;
    //[HideInInspector]
    public PathPoint nextPathPoint;
    [HideInInspector]
    public PathPoint startPathPoint;
    [HideInInspector]
    public Path currentPath;
    [HideInInspector]
    public List<PathPoint> pathPoints;

    private int frameLimit = 10;
    private int frameIncrement = 0;    
    private int playerMask = 1 << 8;
    private int selfMask;

    [HideInInspector]
    public bool lit = false;
    public bool readyToMove = true;

    public Animator animator;
    public GameObject particleEffect;

    public MeshRenderer body;


    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        navAgent = GetComponent<NavMeshAgent>();
        spherePos = gameObject.transform.Find("Eye");
        //eyePos = gameObject.transform.Find("Eyes").transform;
        body = gameObject.transform.Find("bloodhillantagrig").GetComponent<MeshRenderer>();
        eyeGlower = gameObject.transform.Find("bloodhillantagrig").GetComponent<EyeLights>();
        ogStoppingDistance = navAgent.stoppingDistance;
        ogAlertTime = alertTime;
        hearingRange = transform.GetChild(1).GetComponent<EnemyHearing>();
        navAgent.speed = patrolSpeed;
        selfMask = ~(LayerMask.GetMask("Enemy") | LayerMask.GetMask("Ignore Raycast"));
        animator = gameObject.transform.Find("bloodhillantagrig").GetComponent<Animator>();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateBehavior();
        }
    }

    public void SetState(AIState newState)
    {
        if (currentState!= null)
        {
            currentState.ExitBehavior();
        }
        currentState = newState;
        currentState.EntryBehavior();
    }

    public void Sight()
    {
        RaycastHit hit, hat;

        if (player && !gotPlayer && !gameController.playerIsSafe)
        {
            Vector3 lookTarget = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

            Debug.DrawRay(spherePos.position, transform.forward * maxViewDistance, Color.red, 0.5f);
            if (Physics.SphereCast(spherePos.position, fieldOfView, transform.forward, out hit, maxViewDistance, playerMask))
            {
                Debug.DrawRay(eyePos.position, (player.transform.position - transform.position) * maxViewDistance, Color.green, 0.5f);
                if (Physics.Raycast(eyePos.position, (player.transform.position - transform.position), out hat, maxViewDistance, selfMask))
                {
                    //Debug.Log(hat.collider.gameObject);
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
        if (lit)
        {
            playerPosition = player.transform.position;
            aware = true;
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
        Instantiate(particleEffect, transform.position, Quaternion.identity);
        StartCoroutine("TempHide", targetLocation);
        navAgent.Warp(targetLocation);
        //Instantiate(particleEffect, targetLocation, Quaternion.identity);
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
        currentPath = newPath;
        pathPoints = PopulateList(newPath);
        pathDirection = 1;
        startPathPoint = pathPoints[0];
        nextPathPoint = pathPoints[0];
        currentPathPoint = pathPoints[0];
        navAgent.SetDestination(nextPathPoint.transform.position);
    }

    IEnumerator Waiting(float waitTime)
    {
        Debug.Log("Running Wait()");
        if (waitTime > 0)
        {
            yield return new WaitForSeconds(waitTime);
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator Ready()
    {
        Debug.Log("Setting readyToMove to true");
        readyToMove = true;
        yield return null;
    }

    IEnumerator PathWait(float waitTime)
    {
        Debug.Log("Starting PathWait Coroutine");
        Debug.Log(waitTime);
        yield return new WaitForSeconds(waitTime);
        readyToMove = true;
    }

    public void Wait(float waitTime)
    {
        Debug.Log("Setting readyToMove to false");
        readyToMove = false;
        
        StartCoroutine("PathWait", waitTime);
    }

    IEnumerator TempHide(Vector3 effectLocation)
    {
        body.enabled = false;
        yield return new WaitForSeconds(1.1f);
        body.enabled = true;
        Instantiate(particleEffect, effectLocation, Quaternion.identity);
    }
}
