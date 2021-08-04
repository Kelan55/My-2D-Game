using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
	#region Set Variables
	public FiniteStateMachine stateMachine;

	public D_Entity entityData;
	#endregion

	#region Components
	public Rigidbody2D RB { get; private set; }
	public Animator Anim { get; private set; }
	public GameObject AliveGO { get; private set; }
	public AnimationToStatemachine AnimationToStatemachine { get; private set; }
	public Collider2D Collider { get; private set; }

	public AttackDetails attackDetails { get; protected set; }

	public bool IsDead { get; protected set; }
	#endregion

	#region Transforms
	[SerializeField] private Transform wallCheck;
	[SerializeField] private Transform ledgeCheck;
	[SerializeField] private Transform playerCheck;
	[SerializeField] private Transform groundCheck;
	#endregion

	#region Other Variables
	public int FacingDirection { get; private set; }
	public Vector2 CurrentVelocity { get; private set; }  //Um Memory zu sparen, effektiever
	public int LastDamageDirection { get; private set; }

	[SerializeField] public float stunDamageAmount = 1;

	private float currentHealth;
	private float currentStunResistance;
	private float lastDamageTime;

	private Vector2 workspace;

	protected bool isStuned;
	#endregion


	#region Unity Callback Functions
	public virtual void Awake()
	{
		stateMachine = new FiniteStateMachine();
	}

	public virtual void Start()
	{
		FacingDirection = -1;

		AliveGO = transform.Find("Alive").gameObject;
		RB = AliveGO.GetComponent<Rigidbody2D>();
		Anim = AliveGO.GetComponent<Animator>();
		AnimationToStatemachine = AliveGO.GetComponent<AnimationToStatemachine>();
		Collider = AliveGO.GetComponent<Collider2D>();

		IsDead = false;

		currentHealth = entityData.maxHealth;
		currentStunResistance = entityData.stunResistance;
	}

	public virtual void Update()
	{
		CurrentVelocity = RB.velocity;
		stateMachine.CurrentState.LogicUpdate();

		if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
		{
			ResetStunResistance();
		}
	}

	public virtual void FixedUpdate()
	{
		stateMachine.CurrentState.PhysicsUpdate();
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
	public virtual bool CheckWall()
	{
		return Physics2D.Raycast(wallCheck.position, -AliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
	}

	public virtual bool CheckLedge()
	{
		return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
	}

	public virtual bool CheckGround()
	{
		return Physics2D.OverlapCircle(groundCheck.position, entityData.gourndCheckRadius, entityData.whatIsGround);
	}

	public virtual bool CheckPlayerInMinAgroRange()
	{
		return Physics2D.Raycast(playerCheck.position, -AliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
	}

	public virtual bool CheckPlayerInMaxAgroRange()
	{
		return Physics2D.Raycast(playerCheck.position, -AliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
	}

	public virtual bool CheckPlayerInCloseRangeAction()
	{
		return Physics2D.Raycast(playerCheck.position, -AliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
	}
	#endregion

	#region Stun Function
	public virtual void ResetStunResistance()
	{
		isStuned = false;
		currentStunResistance = entityData.stunResistance;
	}
	#endregion

	#region Damge Functions
	public virtual void Damage(AttackDetails attackDetails)
	{
		lastDamageTime = Time.time;

		Debug.Log("Enemy been Hit with " + attackDetails.damageAmount + " Damage!");

		this.attackDetails = attackDetails;

		currentHealth -= attackDetails.damageAmount;
		currentStunResistance -= attackDetails.stunDamageAmount;

		if (currentHealth <= 0)
		{
			Debug.Log("Entity is Dead!");
			IsDead = true;
			currentHealth = 0f;
		}
		else if (attackDetails.position.x > transform.position.x)
		{
			LastDamageDirection = -1;
		}
		else
		{
			LastDamageDirection = 1;
		}

		if (currentStunResistance <= 0)
		{
			isStuned = true;
		}
	}

	public virtual void KnockBack(AttackDetails attackDetails)
	{
		RB.AddForce(new Vector2(LastDamageDirection * attackDetails.knockbackForce.x, attackDetails.knockbackForce.y), ForceMode2D.Impulse);
	}

	public virtual void KnockBack(float velocity, Vector2 angle, int direction)
	{
		angle.Normalize();
		workspace.Set(angle.x * velocity * direction, angle.y * velocity);
		RB.velocity = workspace;
	}
	#endregion

	#region Other Functions
	public void Flip()
	{
		FacingDirection *= -1;
		AliveGO.transform.Rotate(0.0f, 180.0f, 0.0f);
	}
	#endregion

	#region Animation Trigger Events
	private void AnimationTrigger() => stateMachine.CurrentState.AnimationTrigger();
	private void AnimationFinishTrigger() => stateMachine.CurrentState.AnimationFinishTrigger(); //Hier wird der Trigger weitergegeben an die Aktuelle State  und dessen AnimationFinishTrigger
	#endregion

	public virtual void OnDrawGizmos()
	{
		Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * FacingDirection * entityData.wallCheckDistance));
		Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

		Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * FacingDirection * entityData.closeRangeActionDistance), 0.2f);
		Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * FacingDirection * entityData.minAgroDistance), 0.2f);
		Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * FacingDirection * entityData.maxAgroDistance), 0.2f);
		Gizmos.DrawWireSphere(groundCheck.position, entityData.gourndCheckRadius);
	}
}
