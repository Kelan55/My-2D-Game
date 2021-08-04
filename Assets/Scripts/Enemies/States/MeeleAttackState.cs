using System.Collections;
using UnityEngine;

public class MeeleAttackState : AttackState
{
	protected D_MeeleAttackState stateData;

	protected AttackDetails attackDetails;

	public MeeleAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeeleAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
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

		attackDetails.damageAmount = stateData.attackDamage;
		attackDetails.position = entity.AliveGO.transform.position;
		attackDetails.knockbackForce = stateData.knockbackForce;
		attackDetails.stunDamageAmount = entity.stunDamageAmount;
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

		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

		foreach (Collider2D collider in detectedObjects)
		{
			collider.transform.SendMessage("Damage", attackDetails);
		}
	}
}
