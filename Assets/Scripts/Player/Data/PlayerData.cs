using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
	[Header("Player Health")]
	public float maxHealth = 50f;

	[Header("Move State")]
	public float movemementVelocity = 10;
	public float moveWhileAttack = 0f;

	[Header("Jump State")]
	public float jumpVelocity = 15f;
	public int amountOfJumps = 1;

	[Header("Attack Variables")]
	public float attackRadius = 0.5f;
	public float attackDamage = 10f;

	[Header("Attack Move Time")]
	public float attackMoveTime = 0.2f;

	[Header("Attack Damage and Force")]
	public Vector2 knockbackForce;

	[Header("Getting Hit Blink")]
	public float blinkTime = 0.2f;

	[Header("Check Variables")]
	public float groundCheckRadius = 0.3f;
	public LayerMask whatIsGround;
	public LayerMask whatIsEnemy;

	[Header("Stun Variables")]
	public float stunTime = 3f;
	public float stunKnockBackTime = 0.2f;
	public float stunKnockBackSpeed = 20f;
	public Vector2 knockBackAngle;
	public float stunResistance = 3f;
	public float stunRecoveryTime = 2f;
}

