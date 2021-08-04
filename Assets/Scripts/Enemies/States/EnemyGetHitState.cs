using System.Collections;
using UnityEngine;

public class EnemyGetHitState : State
{
	protected D_EnemyGetHitState stateData;

	public EnemyGetHitState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_EnemyGetHitState stateData) : base(entity, stateMachine, animBoolName)
	{
		this.stateData = stateData;
	}

	public override void DoChecks()
	{
		base.DoChecks();
	}

	public override void Enter()
	{
		base.Enter();

		KnockBack(entity.attackDetails);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	public void KnockBack(AttackDetails attackDetails)
	{
		entity.RB.AddForce(attackDetails.knockbackForce, ForceMode2D.Impulse);
	}
}
