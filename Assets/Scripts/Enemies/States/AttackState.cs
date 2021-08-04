using System.Collections;
using UnityEngine;

public class AttackState : State
{
	protected Transform attackPosition;

	protected bool isPlayerInMinAgroRange;

	public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
	{
		this.attackPosition = attackPosition;
	}

	public override void DoChecks()
	{
		base.DoChecks();

		isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
	}

	public override void Enter()
	{
		base.Enter();

		entity.AnimationToStatemachine.attackState = this; //verknüpfung zur dem Attackstate in dem Script!
		isAnimationFinished = false;
		entity.SetVelocityX(0f);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void AnimationFinishTrigger()
	{
		base.AnimationFinishTrigger();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void AnimationTrigger()
	{
		base.AnimationTrigger();
	}
}
