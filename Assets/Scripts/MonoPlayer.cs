using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoPlayer : MonoEntity {
	public static MonoPlayer Instance;
	private void Awake()
	{
		Instance = this;
		return;
	}

	public float Speed = 1f;
	public float rotateSpeed = 1f;
	private Rigidbody _rg;

	private Dictionary<int, MonoAI> _attackTargetDict;
	// Use this for initialization
	void Start () {
		_rg = transform.GetComponent<Rigidbody>();
		_attackTargetDict = new Dictionary<int, MonoAI>();
		return;
	}

	private void OnTriggerEnter(Collider other)
	{
		MonoAI ai = other.transform.GetComponent<MonoAI>();
		if (ai != null && ai.status != AIStatus.Freeze && ai.status != AIStatus.Attack)
		{
			_attackTargetDict[ai.index] = ai;
		}
		return;
	}

	private void OnTriggerExit(Collider other)
	{
		MonoAI ai = other.transform.GetComponent<MonoAI>();
		if (ai != null && ai.status != AIStatus.Freeze && ai.status != AIStatus.Attack)
		{
			_attackTargetDict.Remove(ai.index);
		}
		return;
	}

	private void Update()
	{
		foreach(MonoAI ai in _attackTargetDict.Values)
		{
			if (ai.status == AIStatus.Freeze || ai.status == AIStatus.Attack)
			{
				continue;
			}
			ai.attackTimer += ai.attackTimer + Time.deltaTime;
		}

		return;
	}
	
	private void LateUpdate()
	{
		UpdateInput();

		return;
	}

	private void UpdateInput()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		if (Mathf.Approximately(x * x + y * y, 0f))
		{
			_rg.velocity = Vector3.zero;
			_rg.angularVelocity = Vector3.zero;
			return;
		}

		UpdateRotation(x, y);
		UpdatePosition(y);

		return;
	}

	private void UpdateRotation(float inputX, float inputY)
	{
		if (Mathf.Approximately(Mathf.Abs(inputX), 0f))
		{
			_rg.angularVelocity = Vector3.zero;
			return;
		}

		float angle = Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg;
		angle = Mathf.LerpAngle(0f, angle, rotateSpeed * Time.deltaTime);
		Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
		targetRotation = transform.rotation * targetRotation;
		/*
		Vector3 targetDirection = targetRotation * transform.forward;
		targetDirection = targetDirection.normalized;
		transform.forward = targetDirection;
		*/
		_rg.MoveRotation(targetRotation);

		return;
	}

	private void UpdatePosition(float inputY)
	{
		if (Mathf.Approximately(Mathf.Abs(inputY), 0f))
		{
			_rg.velocity = Vector3.zero;
			return;
		}
		Debug.DrawLine(transform.position, transform.position + transform.forward * 2, Color.red);
		//transform.position += transform.forward * inputY * Speed * Time.deltaTime;
		_rg.velocity = transform.forward * inputY * Speed;

		return;
	}
}
