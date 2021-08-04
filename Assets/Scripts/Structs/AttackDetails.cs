using System.Collections;
using UnityEngine;

public struct AttackDetails
{
	public Vector2 position;
	public float damageAmount;
	public Vector2 knockbackForce;
	public float stunDamageAmount;  //Wieviel mit jedem Schlag von der Resistence abgezogen wird!
}
