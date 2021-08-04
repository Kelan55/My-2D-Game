using System.Collections;
using UnityEngine;

public class PlayerStunState : PlayerState
{
	protected bool isStunTimeOver;
	protected bool isGrounded;
	protected bool isMovementStoped;

	public PlayerStunState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

		isStunTimeOver = false;
		isMovementStoped = false;

		player.KnockBack(playerData.stunKnockBackSpeed, playerData.knockBackAngle, player.lastDamageDirection);
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (Time.time > startTime + playerData.stunTime)
		{
			isStunTimeOver = true;
			player.ResetStunResistance();
		}

		if (isGrounded && Time.time >= startTime + playerData.stunKnockBackTime && !isMovementStoped)
		{
			isMovementStoped = true;
			player.SetVelocityX(0f);
		}
	}

	public override void PhysicUpdate()
	{
		base.PhysicUpdate();
	}
}
