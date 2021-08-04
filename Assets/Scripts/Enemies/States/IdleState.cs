using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
	protected D_IdleState stateData;

	protected float idleTime;

	protected bool isIdleTimeOver;
	protected bool flipAfterIdle;
	protected bool isPlayerInMinAgroRange;
	protected bool isPlayerInMaxAgroRange;

	public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
	{
		this.stateData = stateData;
	}

	public override void DoChecks()
	{
		base.DoChecks();

		isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
		isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
	}

	public override void Enter()
	{
		base.Enter();

		DoChecks();

		entity.SetVelocityX(0f);
		isIdleTimeOver = false;
		SetRandomIdleTime();
	}

	public override void Exit()
	{
		base.Exit();

		if (flipAfterIdle)
		{
			entity.Flip();
		}
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (Time.time >= startTime + idleTime)
		{
			isIdleTimeOver = true;
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();

		DoChecks();
	}

	public void SetFlipAfterIdle(bool flip)
	{
		flipAfterIdle = flip;
	}

	private void SetRandomIdleTime()
	{
		idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
	}
}
