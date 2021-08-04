using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State")]
public class D_MoveState : ScriptableObject
{
	[Header("Move State")]
	public float movemementVelocity = 4f;
	public float moveWhileAttack = 0f;

	[Header("Attack Move Time")]
	public float attackMoveTime = 0.2f;

	[Header("Jump State")]
	public float jumpVelocity = 15f;
	public int amountOfJumps = 1;

	[Header("Check Variables")]
	public float groundCheckRadius = 0.3f;
	public LayerMask whatIsGround;
}
