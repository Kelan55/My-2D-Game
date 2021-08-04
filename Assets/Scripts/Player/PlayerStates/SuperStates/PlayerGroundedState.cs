using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
	protected int xInput;

	private bool jumpInput;
	private bool isGrounded;
	private bool attack;
	private bool isAttacking;

	public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

		player.JumpState.ResetAmountOfJumpsLeft();
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
		isAttacking = player.InputHandler.IsAttacking;

		if (jumpInput && player.JumpState.CanJump())
		{
			player.InputHandler.UseJumpInput();
			stateMachine.ChangeState(player.JumpState);
		}
		else if (!isGrounded)
		{
			player.JumpState.DecreadeAmountOfJumpsLeft();
			stateMachine.ChangeState(player.InAirState);
		}
		else if (attack)
		{
			stateMachine.ChangeState(player.AttackState);
		}
	}

	public override void PhysicUpdate()
	{
		base.PhysicUpdate();
	}
}
