using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyGetHitStateData", menuName = "Data/State Data/Get Hit State")]
public class D_EnemyGetHitState : ScriptableObject
{
	[Header("Attack Damage and Force")]

	public Vector2 knockbackForce;


	[Header("Attack Variables")]
	public float attackRadius = 0.5f;
	public float attackDamage = 10f;
}
