using System.Collections;
using UnityEngine;

public class AnimationToStatemachine : MonoBehaviour
{
	public AttackState attackState;
	public EnemyGetHitState enemyGetHitState;

	private void AnimationTrigger() //Am animationspunkt wo etwas passieren soll 
	{
		if (attackState != null)
		{
			Debug.Log("Enemy Attacks Player");
			attackState.AnimationTrigger();
		}

		if (enemyGetHitState != null)
		{
			enemyGetHitState.AnimationTrigger();
		}
	}

	private void AnimationFinishTrigger() //Am ende der Attack
	{
		if (attackState != null)
		{
			Debug.Log("Enemy Attack Animation Finished");
			attackState.AnimationFinishTrigger();
		}

		if (enemyGetHitState != null)
		{
			enemyGetHitState.AnimationFinishTrigger();
		}
	}
}

