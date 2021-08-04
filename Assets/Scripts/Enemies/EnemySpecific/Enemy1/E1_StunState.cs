using System.Collections;
using UnityEngine;

public class E1_StunState : StunState
{
	Enemy1 enemy;

	public E1_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

		//if (isGrounded)
		//{
		//	entity.SetVelocityX(0f);
		//}
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (isStunTimeOver)
		{
			if (performeCloseRangeAction)
			{
				stateMachine.ChangeState(enemy.MeeleAttackState);
			}
			else if (isPlayerInMinAgroRange)
			{
				stateMachine.ChangeState(enemy.ChargeState);
			}
			else
			{
				enemy.LookForPlayerState.SetTurnImmediatley(true);
				stateMachine.ChangeState(enemy.LookForPlayerState);
			}
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}
}
