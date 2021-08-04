using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
	private AttackDetails attackDetails;


	private bool attackTrigger;

	public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
	{
	}

	public override void DoChecks()
	{
		base.DoChecks();
	}

	public override void Enter()
	{
		base.Enter();

		attackTrigger = false;

		attackDetails.damageAmount = playerData.attackDamage;
		attackDetails.position = player.transform.position;
		attackDetails.knockbackForce = playerData.knockbackForce;
		attackDetails.stunDamageAmount = player.stunDamageAmount;

		player.InputHandler.UsedAttack();

		if (isGrounded)
		{
			player.SetVelocityX(0f);
		}
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		if (!isAnimationFinished && !isGrounded)
		{
			player.SetVelocityX(player.CurrentVelocity.x);
		}
		else if (!isAnimationFinished && isGrounded)
		{
			MoveWhileAttack();
			//player.SetVelocityX(0f);
		}
		else
		{
			stateMachine.ChangeState(player.IdleState);
		}
	}

	public override void PhysicUpdate()
	{
		base.PhysicUpdate();
	}

	private void MoveWhileAttack() //beweget sich nach vorne mit jedem schwung
	{
		if (startTime + playerData.attackMoveTime <= Time.time)
		{
			player.SetVelocityX(playerData.moveWhileAttack * player.FacingDirection);
		}
		else
		{
			player.SetVelocityX(0f);

		}
	}

	public override void AnimationFinishTrigger()
	{
		base.AnimationFinishTrigger();
	}

	public override void AnimationTrigger()
	{
		base.AnimationTrigger();

		Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(player.AttackPosition.position, playerData.attackRadius, playerData.whatIsEnemy);

		foreach (Collider2D collider in detectedObjects)
		{
			Debug.Log("Enemy Detected!!");
			collider.transform.SendMessage("Damage", this.attackDetails);
		}
	}
}
