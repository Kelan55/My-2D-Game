using System.Collections;
using UnityEngine;

public class DeadState : State
{
	protected D_DeadState stateData;

	protected float wait = 1f;

	public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
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
	}

	public override void Enter()
	{
		base.Enter();

		entity.SetVelocityX(0);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (entity.CurrentVelocity.x > 0.1f)
		{
			entity.SetVelocityX(0);
		}

		WaitALittle();
	}

	public override void PhysicsUpdate()
	{
		base.PhysicsUpdate();
	}

	protected void WaitALittle()
	{
		if (startTime + wait < Time.time)
		{
			DisableCollisionAndRigidBody();
		}
	}

	protected void DisableCollisionAndRigidBody()
	{

		entity.RB.bodyType = RigidbodyType2D.Static;
		entity.Collider.enabled = false;
	}

}
