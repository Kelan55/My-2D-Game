using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
	protected bool isAbilityDone; //AbilityState schaut nach ob die Aktion abgeschlossen ist und entscheide dann in welche State es weiter geht. 

	protected bool isGrounded; //InAir und Ability müssen wissen ob ich grounded bin, damit sie in die State wechseln können

	private bool attacking;
	private bool isAttacking;

	public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

		isGrounded = player.CheckIfGrounded();
	}

	public override void Enter()
	{
		base.Enter();

		isAbilityDone = false;
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void LogicUpdate()
	{
		base.LogicUpdate();

		attacking = player.InputHandler.Attack;
		isAttacking = player.InputHandler.IsAttacking;

		if (isAbilityDone) //Wenn ich gepsrungen bin (isAbilityDone kann auch für was anderes als Springen stehen)
		{
			if (isGrounded && player.CurrentVelocity.y <= 0.01f) //Und den Boden berühre (da ich am anfang auch boden berühre, hier den check von y)
			{
				stateMachine.ChangeState(player.IdleState); //Wechsel in Idle State
			}
			else
			{
				stateMachine.ChangeState(player.InAirState);
			}
		}
	}

	public override void PhysicUpdate()
	{
		base.PhysicUpdate();
	}
}
