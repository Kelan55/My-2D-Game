using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
	public Vector2 RawMovementInput { get; private set; }
	public int NormInputX { get; private set; }
	public int NormInputY { get; private set; }
	public bool JumpInput { get; private set; }
	public bool JumpInputStop { get; private set; }
	public bool Attack { get; private set; }
	public bool IsAttacking { get; private set; }

	[SerializeField]
	private float inputHoldTime = 0.2f;
	[SerializeField]
	private float attackResetTime = 1.2f;

	private float jumpInputStartTime;
	private float attackStartTime;

	private void Start()
	{
		Attack = false;
		IsAttacking = false;
	}

	private void Update()
	{
		ResetAttack();
		CheckJumpInputHoldTime();
	}

	public void OnMoveInput(InputAction.CallbackContext context)
	{
		RawMovementInput = context.ReadValue<Vector2>();

		NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
		NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;

	}

	public void OnJumpInput(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			JumpInput = true;
			JumpInputStop = false;
			jumpInputStartTime = Time.time;
		}

		if (context.canceled)
		{
			JumpInputStop = true;
		}
	}

	public void OnAttackInput(InputAction.CallbackContext context)
	{
		if (context.started && !Attack && !IsAttacking)
		{
			Attack = true;
			IsAttacking = true;
			attackStartTime = Time.time;
		}
	}

	private void CheckJumpInputHoldTime()
	{
		if (Time.time >= jumpInputStartTime + inputHoldTime)
		{
			JumpInput = false;
		}
	}

	private void ResetAttack()
	{
		if (Time.time >= attackStartTime + attackResetTime)
		{
			Attack = false;
			IsAttacking = false;
		}
	}

	public void UseJumpInput() => JumpInput = false;
	public void UsedAttack() => Attack = false;


}
