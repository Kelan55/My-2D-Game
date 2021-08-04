using System.Collections;
using UnityEngine;

public class E1_EnemyGetHitState : EnemyGetHitState
{
	private Enemy1 enemy;

	private bool isDead;

	public E1_EnemyGetHitState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_EnemyGetHitState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

		isAnimationFinished = false;
		isDead = entity.IsDead;
		entity.AnimationToStatemachine.enemyGetHitState = this;
	}

	public override void Exit()
	{
		base.Exit();

		isAnimationFinished = false;
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (isAnimationFinished && enemy.IsDead)
		{
			//TODO: Wechsle in Dead State
			stateMachine.ChangeState(enemy.DeadState);
		}
		else if (isAnimationFinished && !enemy.IsDead)
		{
			stateMachine.ChangeState(enemy.LookForPlayerState);
		}
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public override void AnimationFinishTrigger()
	{
		base.AnimationFinishTrigger();
	}
}
