using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region Set Variables
	public PlayerStateMachine StateMachine { get; private set; }

	public PlayerIdleState IdleState { get; private set; }
	public PlayerMoveState MoveState { get; private set; }
	public PlayerJumpState JumpState { get; private set; }
	public PlayerInAirState InAirState { get; private set; }
	public PlayerLandState LandState { get; private set; }
	public PlayerAttackState AttackState { get; private set; }
	public PlayerGetHitState GetHitState { get; private set; }
	public PlayerStunState StunState { get; private set; }


	[SerializeField] public float stunDamageAmount = 1;

	[SerializeField] private PlayerData playerData;
	#endregion

	#region Components
	public Core Core { get; private set; }
	public Animator Anim { get; private set; }
	public PlayerInputHandler InputHandler { get; private set; }
	public Rigidbody2D RB { get; private set; }
	public SpriteRenderer SR { get; private set; }
	public Transform AttackPosition { get; private set; }
	public AttackDetails attackDetails { get; protected set; }
	#endregion

	#region Check Transforms
	[SerializeField] private Transform groundCheck;
	[SerializeField] private Transform attackPosition;
	#endregion

	#region Other Variables
	public Vector2 CurrentVelocity { get; private set; }  //Um Memory zu sparen, effektiever
	public int FacingDirection { get; private set; }

	private Vector2 workspace;

	private float currentHealth;
	private float currentStunResistance;

	private bool isStuned;

	public int lastDamageDirection;
	#endregion

	#region Unity Callback Functions
	private void Awake()
	{
		Core = GetComponentInChildren<Core>();

		StateMachine = new PlayerStateMachine();

		IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
		MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
		JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
		InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
		LandState = new PlayerLandState(this, StateMachine, playerData, "land");
		AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
		GetHitState = new PlayerGetHitState(this, StateMachine, playerData, "getingHit");
		StunState = new PlayerStunState(this, StateMachine, playerData, "stun");
	}

	private void Start()
	{
		Anim = GetComponent<Animator>();
		InputHandler = GetComponent<PlayerInputHandler>();
		RB = GetComponent<Rigidbody2D>();
		SR = GetComponent<SpriteRenderer>();
		AttackPosition = attackPosition;

		FacingDirection = 1;

		currentHealth = playerData.maxHealth;
		currentStunResistance = playerData.stunResistance;

		StateMachine.Initialize(IdleState);
	}

	private void Update()
	{
		CurrentVelocity = RB.velocity; //ein mal aufrufen, um nicht immer RB.velocity aufrufen zu müssen

		StateMachine.CurrentState.LogicUpdate();
	}

	private void FixedUpdate()
	{
		StateMachine.CurrentState.PhysicUpdate();
	}
	#endregion

	#region Set Functions
	public void SetVelocityX(float velocity)
	{
		workspace.Set(velocity, CurrentVelocity.y);
		RB.velocity = workspace;
		CurrentVelocity = workspace;
	}

	public void SetVelocityY(float velocity)
	{
		workspace.Set(CurrentVelocity.x, velocity);
		RB.velocity = workspace;
		CurrentVelocity = workspace;
	}
	#endregion

	#region Check Functions
	public bool CheckIfGrounded()
	{
		return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
	}

	public void CheckIfShouldFlip(int xInput)
	{
		if (xInput != 0 && xInput != FacingDirection)
		{
			Flip();
		}
	}
	#endregion

	#region Damge ans Knockback Functions
	public void Damage(AttackDetails attackDetails)
	{
		Debug.Log("Player been Hit with " + attackDetails.damageAmount + " Damage!");

		this.attackDetails = attackDetails;

		currentHealth -= attackDetails.damageAmount;
		currentStunResistance -= attackDetails.stunDamageAmount;

		if (attackDetails.position.x > transform.position.x)
		{
			lastDamageDirection = -1;
		}
		else
		{
			lastDamageDirection = 1;
		}

		if (currentStunResistance <= 0)
		{
			isStuned = true;
		}

		if (isStuned && StateMachine.CurrentState == this.StunState) //Wenn ich schon gestund bin, dann ignoriere alles 
		{
			return;
		}
		else if (isStuned && StateMachine.CurrentState != this.StunState) //Wenn ich noch nicht gestunt bin, dannn wechsel in Stun State
		{
			StateMachine.ChangeState(StunState);
		}
		else
		{
			StateMachine.ChangeState(GetHitState);
		}
	}

	public void KnockBack(AttackDetails attackDetails)
	{
		RB.AddForce(new Vector2(lastDamageDirection * attackDetails.knockbackForce.x, attackDetails.knockbackForce.y), ForceMode2D.Impulse);
	}

	public virtual void KnockBack(float velocity, Vector2 angle, int direction)
	{
		angle.Normalize();
		workspace.Set(angle.x * velocity * direction, angle.y * velocity);
		RB.velocity = workspace;
	}
	#endregion

	#region Stun Function
	public void ResetStunResistance()
	{
		isStuned = false;
		currentStunResistance = playerData.stunResistance;
	}
	#endregion

	#region Other Functions

	private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
	private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger(); //Hier wird der Trigger weitergegeben an die Aktuelle State  und dessen AnimationFinishTrigger


	private void Flip()
	{
		FacingDirection *= -1;
		transform.Rotate(0.0f, 180.0f, 0.0f);
	}
	#endregion

	#region Gizm
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(attackPosition.position, playerData.attackRadius);
	}
	#endregion
}
