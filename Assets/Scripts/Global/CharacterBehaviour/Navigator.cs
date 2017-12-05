// Author: Mathias Dam Hedelund
// Contributors: Itai Yavin, You Wu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Navigator : MonoBehaviour
{
    public Transform waypoint;
    public Transform splitWaypoint;
    public bool autoRepath;
    public bool drawPath;
    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private bool destinationReached;
    private float previousSpeed;
    private string speedParameter = "speed";
    private bool hasSpeedParameter;
    private GlobalConstantsManager constantsManager;

    #region DEBUG
#if UNITY_EDITOR
    private LineRenderer lineRenderer;
#endif
    #endregion

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        #region DEBUG
#if UNITY_EDITOR
        lineRenderer = GetComponent<LineRenderer>();
#endif
        #endregion

        constantsManager = GlobalConstantsManager.GetInstance();

        navMeshAgent.speed = constantsManager.constants.speed;
        navMeshAgent.acceleration = constantsManager.constants.acceleration;
        navMeshAgent.height = constantsManager.constants.height;
        navMeshAgent.radius = constantsManager.constants.radius;

        navMeshAgent.autoRepath = autoRepath;
        SetDestination();
        hasSpeedParameter = HasParameter(speedParameter, animator);
    }

    private void Update()
    {
        if (navMeshAgent.isStopped)
        {
            return;
        }
        float currentSpeed = navMeshAgent.velocity.magnitude;
        
        if (hasSpeedParameter)
        {
            animator.SetFloat(speedParameter, Mathf.Lerp(1, 2, (currentSpeed - 1) / 2));
        }

    }

    public void SetSplitPath()
    {
        navMeshAgent.SetDestination(splitWaypoint.position);
    }

    public void Move(Vector3 direction)
    {
        navMeshAgent.Move(direction * navMeshAgent.speed * Time.deltaTime);
    }

    public void SetDestination()
    {
        SetDestination(waypoint);
    }

    public void SetSplitWaypoint(Transform waypoint)
    {
        splitWaypoint = waypoint;
    }

    public void SetDestination(Transform destination)
    {
        if (destination == null)
        {
            return;
        }
        else
        {
            waypoint = destination;
            navMeshAgent.SetDestination(destination.position);
        }

        #region DEBUG
#if UNITY_EDITOR
        if (drawPath)
        {
            StartCoroutine(GetPath());
        }
#endif
        #endregion
    }

    public Transform GetDestination()
    {
        return waypoint;
    }

    public float GetSpeed()
    {
        return navMeshAgent.speed;
    }

    public void SetSpeed(float speed)
    {
        if (hasSpeedParameter)
        {
            //animator.SetFloat(speedParameter, speed < 2 ? speed : 2);
        }
        previousSpeed = navMeshAgent.speed;
        navMeshAgent.speed = speed;
    }
    public static bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }

    public void SetPreviousSpeed()
    {
        SetSpeed(previousSpeed);
    }

    public void StopMovement()
    {
        if (!navMeshAgent.isStopped)
        {
            if (hasSpeedParameter)
            {
                animator.SetFloat(speedParameter, 0);
            }
            navMeshAgent.isStopped = true;
        }
    }

    public void ResumeMovement()
    {
        if (navMeshAgent.isStopped)
        {
            navMeshAgent.isStopped = false;
        }
    }

    public bool CheckDestinationReached()
    {
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    #region DEBUG
#if UNITY_EDITOR
    IEnumerator GetPath()
    {
        if (lineRenderer)
        {
            lineRenderer.SetPosition(0, transform.position);
            yield return new WaitForEndOfFrame();
            DrawPath(navMeshAgent.path);
        }
    }

    void DrawPath(NavMeshPath path)
    {
        if (path.corners.Length < 2)
        {
            return;
        }

        lineRenderer.positionCount = path.corners.Length;

        for (int i = 1; i < path.corners.Length; i++)
        {
            lineRenderer.SetPosition(i, path.corners[i]);
        }
    }
#endif
    #endregion
}
