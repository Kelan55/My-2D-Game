using System.Collections;
using UnityEngine;

public class LookForPlayerState : State
{
	protected D_LookForPlayerState stateData;

	protected bool turnImmediatly;
	protected bool isPlayerInMinAgroRange;
	protected bool isAllTurnsDone;
	protected bool isAllTunsTimeDone;

	protected float lastTurnTime;

	protected int amountOfTurnsDone;

	public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animBoolName)
	{
		this.stateData = stateData;
	}

	public override void DoChecks()
	{
		base.DoChecks();

		isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
	}

	public override void Enter()
	{
		base.Enter();

		isAllTurnsDone = false;
		isAllTunsTimeDone = false;

		lastTurnTime = startTime;
		amountOfTurnsDone = 0;

		entity.SetVelocityX(0f);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (turnImmediatly)
		{
			entity.Flip();
			lastTurnTime = Time.time;
			amountOfTurnsDone++;
			turnImmediatly = false;
		}
		else if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && !isAllTurnsDone)
		{
			entity.Flip();
			lastTurnTime = Time.time;
			amountOfTurnsDone++;
		}

		if (amountOfTurnsDone >= stateData.amountOfTurns)
		{
			isAllTurnsDone = true;
		}

		if (Time.time >= lastTurnTime + stateData.timeBetweenTurns && isAllTurnsDone)
		{
			isAllTunsTimeDone = true;
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public void SetTurnImmediatley(bool flip)
	{
		turnImmediatly = flip;
	}
}
