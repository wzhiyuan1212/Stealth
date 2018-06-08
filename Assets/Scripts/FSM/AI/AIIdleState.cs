using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : StateMachineBehaviour {
	private MonoAI _owner;
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_owner = animator.transform.GetComponent<MonoAI>();
		_owner.status = AIStatus.Patral;

		return;
	}
}
