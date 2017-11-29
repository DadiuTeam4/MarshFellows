// Author: Mathias Dam Hedelund
// Contributors:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour 
{
	private float maxY;
	private float flightAngle;
	private Animator animator;
	private bool flying;
	private Vector3 direction;
	private BirdGenerator birdGenerator;
	private float speedScalar;

	private void Awake()
	{
		birdGenerator = BirdGenerator.GetInstance();
		animator = GetComponentInChildren<Animator>();
	}

	private void Start()
	{
		flightAngle = birdGenerator.flightAngle + Random.Range(-1f, 1f) * birdGenerator.flightAngleRandomRange;
		speedScalar = Random.Range(birdGenerator.minSpeedScalar, birdGenerator.maxSpeedScalar);
		maxY = birdGenerator.maxY;
	}

	public void Fly(Vector3 direction)
	{
		this.direction = direction;
		flying = true;

		transform.rotation = Quaternion.LookRotation(direction);

		animator.SetBool("flying", true);
	}

	private void Update()
	{
		if (!flying)
		{
			return;
		}
		float progress = transform.position.y / maxY;
		float speed = birdGenerator.speedCurve.Evaluate(progress) * speedScalar;
		float angle = birdGenerator.angleCurve.Evaluate(progress) * flightAngle;
		transform.position += new Vector3(direction.x * Time.deltaTime, angle * Time.deltaTime, direction.z * Time.deltaTime) * speed;
		if (transform.position.y > maxY)
		{
			Destroy(gameObject);
		}
	}
}
