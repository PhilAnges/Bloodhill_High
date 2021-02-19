using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{    
    [HideInInspector]
    public NavMeshAgent navAgent;
    [HideInInspector]
    public GameController gameController;
    [HideInInspector]
    public Vector3 currentDestination;
    //[HideInInspector]
    public bool aware = false;
    [HideInInspector]
    public float arriveDistance;
    [HideInInspector]
    public int pathDirection = 1;
    [HideInInspector]
    public int nextPoint;
    [HideInInspector]
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
    [HideInInspector]
    public float ogSpeed;

    public Vector3 playerDirection;

    private int frameLimit = 10;
    private int frameIncrement = 0;
    

    private int playerMask = 1 << 8;

    public float fieldOfView = 2f;
    public float maxViewDistance = 10f;
    public float alertTime = 3f;
    public float aggroTime = 10f;
    public float rotationSpeed = 5f;   
    public float playerChaseStoppingDistance;
    public float chaseSpeedMultiplier = 2f;
    public Transform[] pathPoints;
    public Transform spherePos;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        navAgent = GetComponent<NavMeshAgent>();
        ogStoppingDistance = navAgent.stoppingDistance;
        ogAlertTime = alertTime;
        ogSpeed = navAgent.speed;
        nextPoint = 0;
        previousPoint = 0;
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

        Debug.DrawRay(transform.position, transform.forward * maxViewDistance, Color.red, 0.5f);
        if (Physics.SphereCast(spherePos.position, fieldOfView, transform.forward, out hit, maxViewDistance, playerMask))
        {           
            if (Physics.Raycast(transform.position, (hit.transform.position - transform.position), out hat, maxViewDistance))
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

                    awareness += Time.deltaTime;
                    return;
                }
                else
                {
                    aware = false;
                }
            }
        }
        if (awareness > 0)
        {
            awareness -= Time.deltaTime;
        }
    }

    public void Hearing()
    {

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
        transform.position = targetLocation;
    }
}
