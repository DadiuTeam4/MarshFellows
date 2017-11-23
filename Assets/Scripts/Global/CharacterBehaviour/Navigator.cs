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
			Debug.LogError("Destination(transform) is null!");
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
		return navMeshAgent.velocity.magnitude;
	}

    public void SetSpeed(float speed)
    {
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
		lineRenderer.SetPosition(0, transform.position);
		yield return new WaitForEndOfFrame();
		DrawPath(navMeshAgent.path);
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
