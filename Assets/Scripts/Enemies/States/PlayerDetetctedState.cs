using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetetctedState : State
{
	protected D_PlayerDetected stateData;

	protected bool isPlayerInMinAgroRange;
	protected bool isPlayerInMaxAgroRange;
	protected bool performeLongRangeAction;
	protected bool performeCloseRangeAction;

	public PlayerDetetctedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
	{
		this.stateData = stateData;
	}

	public override void DoChecks()
	{
		base.DoChecks();

		isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
		isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();

		performeCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
	}

	public override void Enter()
	{
		base.Enter();

		DoChecks();

		performeLongRangeAction = false;
		entity.SetVelocityX(0f);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (Time.time >= startTime + stateData.longRangeActionTime)
		{
			performeLongRangeAction = true;
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();

		DoChecks();
	}

}
