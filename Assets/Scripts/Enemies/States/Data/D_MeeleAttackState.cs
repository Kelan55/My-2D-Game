using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "newMeeleAttackStateData", menuName = "Data/State Data/Meele Attack State")]
public class D_MeeleAttackState : ScriptableObject
{
	[Header("Attack variables")]
	public float attackRadius = 0.5f;
	public float attackDamage = 10f;

	[Header("Layer Mask Checks")]
	public LayerMask whatIsPlayer;

	[Header("Attack Damage Force")]
	public Vector2 knockbackForce;
}
