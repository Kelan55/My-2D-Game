using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
	[Header("Entity Health")]
	public float maxHealth = 50f;

	[Header("Suroundings Check Variable")]
	public float wallCheckDistance = 0.2f;
	public float ledgeCheckDistance = 0.4f;
	public float gourndCheckRadius = 0.3f;
	public LayerMask whatIsGround;
	public LayerMask whatIsPlayer;

	[Header("Agro Distances")]
	public float maxAgroDistance = 4f;
	public float minAgroDistance = 2f;

	[Header("Action Distances")]
	public float closeRangeActionDistance = 1f;

	[Header("Stun Variables")]
	public float stunResistance = 3f;
	public float stunRecoveryTime = 2f;
}
