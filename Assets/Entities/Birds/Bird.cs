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
	private GlobalConstants constants;

	private void Awake()
	{
		constants = GlobalConstantsManager.GetInstance().constants;
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		flightAngle = constants.flightAngle + Random.Range(-1f, 1f) * constants.flightAngleRandomRange;
		maxY = constants.maxY;
	}

	public void Fly(Vector3 direction)
	{
		this.direction = direction;
		flying = true;
		animator.SetBool("Flying", true);
	}

	private void Update()
	{
		if (!flying)
		{
			return;
		}
		float progress = transform.position.y / maxY;
		float speed = constants.speedCurve.Evaluate(progress);
		float angle = constants.angleCurve.Evaluate(progress) * flightAngle;
		transform.position += new Vector3(direction.x * Time.deltaTime, angle * Time.deltaTime, direction.z * Time.deltaTime) * speed;
		if (transform.position.y > maxY)
		{
			Destroy(gameObject);
		}
	}
}
