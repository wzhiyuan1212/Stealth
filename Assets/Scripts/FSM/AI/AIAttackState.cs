using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : StateMachineBehaviour {
	private MonoAI _owner;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_owner = animator.transform.GetComponent<MonoAI>();
		_owner.status = AIStatus.Attack;
		_owner.SetPursueSpeed();

		return;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_owner.AttackUpdate();
		return;
	}
}
