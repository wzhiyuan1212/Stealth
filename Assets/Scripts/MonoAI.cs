using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIStatus
{
	Idle = 1,
	Patral = 2,
	Attack = 3,
	Freeze = 4
}

public class MonoAI : MonoEntity{
	public int index;
	public MonoSector detector;
	public AIStatus status = AIStatus.Patral;
	public Vector3[] path;
	private int _currentPathIndex = 0;
	private NavMeshAgent _agent;
	private Animator _animator;

	private const float killTime = 5f;
	private float _attackTimer = 0f;
	public float attackTimer
	{
		get
		{
			return _attackTimer;
		}
		set
		{
			if (value > killTime)
			{
				_animator.SetTrigger("Killed");
				_attackTimer = 0f;
			}
			else
			{
				_attackTimer = value;
			}
		}
	}

	void Start () {
		detector = transform.GetComponentInChildren<MonoSector>();
		detector.Init(this);
		entityType = EntityType.AI;
		_agent = transform.GetComponent<NavMeshAgent>();
		if (path.Length > 0)
		{
			_agent.SetDestination(path[0]);
		}
		_animator = transform.GetComponent<Animator>();
		switch(status)
		{
			case AIStatus.Patral:
				_animator.CrossFade("Patrol", 0.1f);
				break;
		}

		return;
	}

	private void Update()
	{
		return;
	}

	public void SetDetectorEnable(bool isEnable)
	{
		//detector.enabled = isEnable;
		return;
	}

	public void SetEnable(bool isEnable)
	{
		_agent.isStopped = !isEnable;
		if (!isEnable)
		{
			_animator.SetTrigger("LevelEnd");
		}

		return;
	}

	public void OnDetectEnemy()
	{
		_animator.SetTrigger("DetectEnemy");

		return;
	}

	public void PatrolUpdate()
	{
		//if (Mathf.Approximately((transform.position - path[_currentPathIndex]).sqrMagnitude, 0f))
		if (!_agent.hasPath)
		{
			_agent.SetDestination(GetNextIndex());
		}

		return;
	}

	public void SetPursueSpeed()
	{
		_agent.speed = 1.2f;
		return;
	}

	public void AttackUpdate()
	{
		_agent.SetDestination(MonoPlayer.Instance.transform.position);
		if ((transform.position - MonoPlayer.Instance.transform.position).sqrMagnitude < 1.2f)
		{
			MonoAIManager.Instance.OnLevelEnd(false);
		}

		return;
	}

	private Vector3 GetNextIndex()
	{
		++_currentPathIndex;
		if (_currentPathIndex < path.Length)
		{
			return path[_currentPathIndex];
		}
		else if (_currentPathIndex < 2 * path.Length - 1)
		{
			return path[2 * path.Length - _currentPathIndex -2];
		}
		else
		{
			_currentPathIndex = 0;
			return path[_currentPathIndex];
		}
	}
}
