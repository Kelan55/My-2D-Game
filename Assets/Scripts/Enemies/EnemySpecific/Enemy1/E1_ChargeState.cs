using System.Collections;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
	private Enemy1 enemy;

	public E1_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
		else if (!isDetectingLedge || isDetectingWall)
		{
			stateMachine.ChangeState(enemy.LookForPlayerState);
		}
		else if (isChargeTimeOver)
		{
			if (isPlayerInMinAgroRange)
			{
				stateMachine.ChangeState(enemy.PlayerDetectedState);
			}
			else
			{
				stateMachine.ChangeState(enemy.LookForPlayerState);
			}
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
