using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/Look for Player State")]
public class D_LookForPlayerState : ScriptableObject
{
	public int amountOfTurns = 2;
	public float timeBetweenTurns = 0.75f;
}
