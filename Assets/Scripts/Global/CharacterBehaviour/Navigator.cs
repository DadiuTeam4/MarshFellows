// Author: Mathias Dam Hedelund
// Contributors: Itai Yavin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Events;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Navigator : MonoBehaviour 
{
	[HideInInspector] public Transform currentWaypoint; 

	public Transform waypoint;
    public Transform splitWaypoint;
	public bool autoRepath;
	public bool drawPath;

    private NavMeshAgent navMeshAgent;
	private bool destinationReached;

    private float previousSpeed;
    private Animator animator;

	private GlobalConstantsManager constantsManager;

	#region DEBUG
	#if UNITY_EDITOR
	private LineRenderer lineRenderer;
	#endif
	#endregion

	private void Awake() 
	{
		constantsManager = GlobalConstantsManager.GetInstance();

		navMeshAgent = GetComponent<NavMeshAgent>();	

		navMeshAgent.speed = constantsManager.constants.speed;
		navMeshAgent.acceleration = constantsManager.constants.acceleration;
		navMeshAgent.height = constantsManager.constants.height;
		navMeshAgent.radius = constantsManager.constants.radius;

        animator = GetComponentInChildren<Animator>();
	}

	private void Start()
	{
		#region DEBUG
		#if UNITY_EDITOR
		lineRenderer = GetComponent<LineRenderer>();
		#endif
		#endregion
		navMeshAgent.autoRepath = autoRepath;
        SetDestination();

    }


    public void SetSplitPath()
	{
        SetDestination(splitWaypoint.transform);
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
			currentWaypoint = destination;
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
		return currentWaypoint;
	}

	public float GetSpeed()
	{
		return navMeshAgent.speed;
	}

    public void SetSpeed(float speed)
    {
        float t = (speed - 1) / 3;
        animator.SetFloat("speed", Mathf.Lerp(1, 2, t));
        previousSpeed = navMeshAgent.speed;
        navMeshAgent.speed = speed;
    }

    public void SetPreviousSpeed()
    {
        navMeshAgent.speed = previousSpeed;
    }

	public void StopMovement()
	{
		if (!navMeshAgent.isStopped)
		{
            animator.SetFloat("speed", 0);
			navMeshAgent.isStopped = true;
		}
	}

	public void ResumeMovement()
	{
		if (navMeshAgent.isStopped)
		{
            SetSpeed(navMeshAgent.speed); //To set animation to correct value.
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
