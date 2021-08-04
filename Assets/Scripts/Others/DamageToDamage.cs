using System.Collections;
using UnityEngine;

public class DamageToDamage : MonoBehaviour
{
	private void Damage(AttackDetails attackDetails)
	{
		transform.parent.SendMessage("Damage", attackDetails);
	}

}
