using System.Collections;
using UnityEngine;

public class Enemy1 : Entity
{
	public E1_IdleState IdleState { get; private set; }
	public E1_MoveState MoveState { get; private set; }
	public E1_PlayerDetectedState PlayerDetectedState { get; private set; }
	public E1_ChargeState ChargeState { get; private set; }
	public E1_LookForPlayerState LookForPlayerState { get; private set; }
	public E1_MeeleAttackState MeeleAttackState { get; private set; }
	public E1_EnemyGetHitState EnemyGetHitState { get; private set; }
	public E1_StunState StunState { get; private set; }
	public E1_DeadState DeadState { get; private set; }


	[SerializeField] private D_IdleState idleStateData;
	[SerializeField] private D_MoveState moveStateData;
	[SerializeField] private D_PlayerDetected playerDetectedData;
	[SerializeField] private D_ChargeState chargeStateData;
	[SerializeField] private D_LookForPlayerState lookForPlayerStateData;
	[SerializeField] private D_MeeleAttackState meeleAttackStateData;
	[SerializeField] private D_EnemyGetHitState enemygetHitStateData;
	[SerializeField] private D_StunState stunStateData;
	[SerializeField] private D_DeadState deadStateData;

	[SerializeField] private Transform meeleAttackPosition;



	public override void Start()
	{
		base.Start();

		MoveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
		IdleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
		PlayerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
		ChargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
		LookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
		MeeleAttackState = new E1_MeeleAttackState(this, stateMachine, "meeleAttack", meeleAttackPosition, meeleAttackStateData, this);
		EnemyGetHitState = new E1_EnemyGetHitState(this, stateMachine, "getHit", enemygetHitStateData, this);
		StunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);
		DeadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);

		stateMachine.Initialize(MoveState);
	}


	public override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.DrawWireSphere(meeleAttackPosition.position, meeleAttackStateData.attackRadius);
	}

	public override void Damage(AttackDetails attackDetails)
	{
		base.Damage(attackDetails);

		if (stateMachine.CurrentState == StunState || stateMachine.CurrentState == DeadState)
		{
			return;
		}
		else if (isStuned && stateMachine.CurrentState != StunState)
		{
			stateMachine.ChangeState(StunState);
		}
		else
		{
			stateMachine.ChangeState(EnemyGetHitState);
		}

	}
}
