using System.Collections;
using UnityEngine;

public class PlayerGetHitState : PlayerState
{

	public PlayerGetHitState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
	{
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

		KnockBack(player.attackDetails);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (isAnimationFinished)
		{
			stateMachine.ChangeState(player.IdleState);
		}
	}

	public override void PhysicUpdate()
	{
		base.PhysicUpdate();
	}

	public void KnockBack(AttackDetails attackDetails)
	{
		player.RB.AddForce(new Vector2(player.lastDamageDirection * attackDetails.knockbackForce.x, attackDetails.knockbackForce.y), ForceMode2D.Impulse);
	}

}
