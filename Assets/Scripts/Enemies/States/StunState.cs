using System.Collections;
using UnityEngine;

public class StunState : State
{
	protected D_StunState stateData;

	protected bool isStunTimeOver;
	protected bool isGrounded;
	protected bool isMovementStoped;
	protected bool performeCloseRangeAction;
	protected bool isPlayerInMinAgroRange;

	public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
	{
		this.stateData = stateData;
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

		isGrounded = entity.CheckGround();
		performeCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
		isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
	}

	public override void Enter()
	{
		base.Enter();

		isStunTimeOver = false;
		isMovementStoped = false;

		entity.KnockBack(stateData.stunKnockBackSpeed, stateData.knockBackAngle, entity.LastDamageDirection);
	}

	public override void Exit()
	{
		base.Exit();

		entity.ResetStunResistance();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (Time.time > startTime + stateData.stunTime)
		{
			isStunTimeOver = true;
		}

		if (isGrounded && Time.time >= startTime + stateData.stunKnockBackTime && !isMovementStoped)
		{
			isMovementStoped = true;
			entity.SetVelocityX(0f);
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
