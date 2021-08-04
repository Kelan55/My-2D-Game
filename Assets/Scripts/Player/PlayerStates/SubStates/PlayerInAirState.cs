using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
	private int xInput; // Damit ich mich in der Luft bewegen kann
	private bool isGrounded; //InAir State muss wissen ob ich boden berühre, damit ich state ändern kann 
	private bool jumpInput;
	private bool attack;


	public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
	{
	}

	public override void DoChecks()
	{
		base.DoChecks();

		isGrounded = player.CheckIfGrounded();
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

		xInput = player.InputHandler.NormInputX;
		jumpInput = player.InputHandler.JumpInput;
		attack = player.InputHandler.Attack;

		if (isGrounded && player.CurrentVelocity.y < 0.01f)
		{
			stateMachine.ChangeState(player.LandState);
		}
		else if (jumpInput && player.JumpState.CanJump())
		{
			stateMachine.ChangeState(player.JumpState);
		}
		else if (attack)
		{
			stateMachine.ChangeState(player.AttackState);
		}
		else
		{
			player.CheckIfShouldFlip(xInput);
			player.SetVelocityX(playerData.movemementVelocity * xInput); //In der Luft bewegen

			player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
			player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
		}
	}

	public override void PhysicUpdate()
	{
		base.PhysicUpdate();
	}
}
