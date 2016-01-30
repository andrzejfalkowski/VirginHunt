using UnityEngine;
using System.Collections;

public class AltarBarrier : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collider)
	{
		Beast beast = collider.GetComponent<Beast>();
		if(beast != null && beast.ReadyToAttack())
		{
			//Debug.Log ("Globals.POWER = " + Globals.POWER);
			Globals.POWER = Mathf.Max(0f, Globals.POWER - Globals.BEAST_POWER);
			if(Globals.POWER > 0f)
			{
				beast.Die();
			}
		}
	}
}
