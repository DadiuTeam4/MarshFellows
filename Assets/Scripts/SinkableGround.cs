// Author: Itai Yavin
// Contributors: Peter

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Events;

public class SinkableGround : Holdable 
{
	[Header("Updating")]
	[Tooltip("If true the points which are sunk will update when touch is moved, instead of staying at the same points.")]
	public bool updateSinkPoints = true;

	[Tooltip("The minimum distance between touch positions before a new set of vertices are found")]
	public float minimumTouchDistance = 1.0f;
	
	[Tooltip("The rate of which the mesh is updated. If set to 5 then each fifth frame, etc.")]
	public int updateRate = 10;
	
	[Header("Obstacles")]
	[Tooltip("Whether there should be placed navmesh obstacles when sinking the ground or not")]
	public bool placeObstacles = true;

	[Tooltip("Controls the size of the obstacles")]
	public float obstacleRadius = 2.0f;

	[Header("Sinking")]
	[Tooltip("The speed of which sinking happens")]
	[Range(0.01f, 1.0f)]
	public float sinkSpeed = 0.1f;
	[Tooltip("The amount of time before sinking")]
	public float sinkDelay = 0.0f;
	[Tooltip("Finds all vertices within this radius")]
	public float radius = 2.0f;
	[Tooltip("The maximum depth that vertices will sink to")]
	public float depth = 0.5f;

	[Header("Rising")]
	[Tooltip("The speed of which the ground will rise again")]
	public float riseSpeed = 0.1f;
	[Tooltip("The amount of seconds before the ground will rise again")]
	public float riseDelay = 2.0f;

	[Header("Eventcalling")]
	[Tooltip("If true the sinkable ground will call an extra event on touch")]
	public bool callExtra;
	[Tooltip("Which event is called, as an extra event")]
	public CustomEvent extraEvent;

	private MeshCollider meshCollider;
	private MeshFilter meshFilter;
	private Mesh mesh;
	private Vector3[] originalVerticePositions;
	private Obstacle[] verticeObstacles;
	private Vector3[] currentVertices;
	private float[] verticeTimes;
	private bool verticesWaiting = false;
	private bool madeChange = false;

	private Vector3 oldTouchPosition;

	private List<Pair<int, float>> nearestPoints = new List<Pair<int, float>>();

	private EventArgument argument = new EventArgument();
	
	void Start () {
		meshCollider = transform.GetChild(0).GetComponent<MeshCollider>();
		meshFilter = GetComponent<MeshFilter>();

		mesh = meshFilter.mesh;
		meshCollider.sharedMesh = mesh;
		originalVerticePositions = mesh.vertices;
		verticeObstacles = new Obstacle[originalVerticePositions.Length];
		currentVertices = mesh.vertices;

		verticeTimes = new float[originalVerticePositions.Length];
		argument.gameObjectComponent = gameObject;
	}
	
	void Update () 
	{
		if(verticesWaiting && Time.frameCount % updateRate == 0)
		{
			RiseGround();
		}
	}

	public override void OnTouchBegin(RaycastHit hit) 
	{
		FindNearestPoints(hit);

		EventManager.GetInstance().CallEvent(CustomEvent.SinkGround, argument);

		if (callExtra)
		{
			EventManager.GetInstance().CallEvent(extraEvent,argument);
		}
	}
	
	public override void OnTouchHold(RaycastHit hit) 
	{
		SinkGround(hit);
	}
	
	public override void OnTouchReleased() 
	{

	}

	private class Obstacle
	{
		public Obstacle(Vector3 position, float radius)
		{
			obstacle = new GameObject();
			obstacle.transform.position = position;
			navMeshObstacleComponent = obstacle.AddComponent<NavMeshObstacle>();
			navMeshObstacleComponent.carving = true;
			navMeshObstacleComponent.shape = NavMeshObstacleShape.Capsule;

			navMeshObstacleComponent.radius = radius;

			enabled = true;
		}

		public GameObject obstacle;
		public NavMeshObstacle navMeshObstacleComponent;

		public bool enabled;

		public void Disable()
		{
			navMeshObstacleComponent.enabled = false;
			enabled = false;
		}

		public void Enable()
		{
			navMeshObstacleComponent.enabled = true;
			enabled = true;
		}

		public void Remove()
		{
			Destroy(obstacle);
		}
	}


	private void FindNearestPoints(RaycastHit hit)
	{
		nearestPoints.Clear();

		Vector3 pointHit = hit.point;
		oldTouchPosition = pointHit;
		pointHit = transform.InverseTransformPoint(pointHit);

		mesh = meshFilter.mesh;
		currentVertices = mesh.vertices;
		Vector3[] normals = mesh.normals;

		float distance;
		for(int i = 0; i < currentVertices.Length; i++)
		{
			distance = Vector3.Distance(currentVertices[i], pointHit);
			if (distance < radius)
			{
				Pair<int, float> newPoint = new Pair<int, float>(i, distance);
				nearestPoints.Add(newPoint);
			}
		}
		if (placeObstacles)
		{
			MakeObstacle();
		}

		argument.vectorComponent = pointHit;
	}

	private void MakeObstacle()
	{
		OrderPairList(ref nearestPoints);
		Vector3 obstaclePosition = transform.TransformPoint(originalVerticePositions[nearestPoints[0].GetFirst()]);
		if (verticeObstacles[nearestPoints[0].GetFirst()] == null){
			verticeObstacles[nearestPoints[0].GetFirst()] = new Obstacle(obstaclePosition, obstacleRadius);
			verticeObstacles[nearestPoints[0].GetFirst()].obstacle.transform.SetParent(transform.GetChild(0));
		}
	}

	private void SinkGround(RaycastHit hit)
	{
		if (updateSinkPoints && Vector3.Distance(hit.point, oldTouchPosition) > minimumTouchDistance)
		{
			FindNearestPoints(hit);
		}

		if (Time.frameCount % updateRate == 0)
		{
			currentVertices = mesh.vertices;

			if(Vector3.Distance(currentVertices[nearestPoints[0].GetFirst()], originalVerticePositions[nearestPoints[0].GetFirst()]) < depth)
			{
				foreach (Pair<int, float> pair in nearestPoints)
				{
					currentVertices[pair.GetFirst()].y -= sinkSpeed * ((radius - pair.GetSecond()) / radius);
					verticeTimes[pair.GetFirst()] = Time.time + riseDelay;	

					verticesWaiting = true;
				}
				
				mesh.vertices = currentVertices;
				mesh.RecalculateNormals();

				meshCollider.sharedMesh = mesh;
			}
		}
	}

	private void RiseGround()
	{
		verticesWaiting = false;
		for (int i = 0; i < verticeTimes.Length; i++)
		{
			if (verticeTimes[i] != 0.0f)
			{
				verticesWaiting = true;

				if (Time.time > verticeTimes[i])
				{
					if (currentVertices[i].y + riseSpeed < originalVerticePositions[i].y)
					{
						currentVertices[i].y += riseSpeed;
					}
					else
					{
						currentVertices[i] = originalVerticePositions[i];
						verticeTimes[i] = 0.0f;

						if (verticeObstacles[i] != null)
						{
							verticeObstacles[i].Remove();
							verticeObstacles[i] = null;
						}
					}

					madeChange = true;
				}
			}
		}

		if (madeChange)
		{
			mesh.vertices = currentVertices;
			mesh.RecalculateNormals();

			meshCollider.sharedMesh = mesh;

			madeChange = false;
		}
	}
	
	private void OrderPairList(ref List<Pair<int, float>> pairList)
	{
		Pair<int, float> temporary;
		for (int i = 1; i < pairList.Count; i++)
		{
			for(int j = 0; j < pairList.Count - i; j++)
			{
				if (pairList[j].GetSecond() > pairList[j + 1].GetSecond())
				{
					temporary = pairList[j];
					pairList[j] = pairList[j + 1];
					pairList[j + 1] = temporary;
				}
			}
		}
	}

	private struct Pair<T, G>
	{
		public Pair(T t, G g)
		{
			value1 = t;
			value2 = g;
		}

		T value1;
		G value2;

		public T GetFirst()
		{
			return value1;
		}

		public G GetSecond()
		{
			return value2;
		}

		public void SetFirst(T t)
		{
			value1 = t;
		}

		public void SetSecond(G g)
		{
			value2 = g;
		}

		public void Print()
		{
			Debug.Log("First: " + value1 + " - Second: " + value2);
		}
	}
}
