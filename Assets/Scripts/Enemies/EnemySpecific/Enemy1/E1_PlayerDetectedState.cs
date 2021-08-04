using System.Collections;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetetctedState
{
	private Enemy1 enemy;

	public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
	{
		this.enemy = enemy;
	}

	public override void DoChecks()
	{
		base.DoChecks();
	}

	public override void Enter()
	{
		base.Enter();
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (performeCloseRangeAction)
		{
			stateMachine.ChangeState(enemy.MeeleAttackState);
		}
		else if (performeLongRangeAction)
		{
			stateMachine.ChangeState(enemy.ChargeState);
		}
		else if (!isPlayerInMaxAgroRange)
		{
			stateMachine.ChangeState(enemy.LookForPlayerState);
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
