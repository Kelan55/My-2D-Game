using System.Collections;
using UnityEngine;

public class E1_DeadState : DeadState
{
	private Enemy1 enemy;

	public E1_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
	{
		this.enemy = enemy;
	}

	public override void AnimationFinishTrigger()
	{
		base.AnimationFinishTrigger();
	}

	public override void AnimationTrigger()
	{
		base.AnimationTrigger();
	}

	public override void DoChecks()
	{
		base.DoChecks();
	}

	public override void Enter()
	{
		base.Enter();

		enemy.SetVelocityX(0);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		Object.Destroy(this.enemy.gameObject, 4f);
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
