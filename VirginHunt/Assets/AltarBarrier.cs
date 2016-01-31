using UnityEngine;
using System.Collections;

public class AltarBarrier : MonoBehaviour 
{
	public SpriteRenderer VisibleAura;

	void OnTriggerEnter2D(Collider2D collider)
	{
		Beast beast = collider.GetComponent<Beast>();
		if(beast != null && beast.ReadyToAttack())
		{
			//Debug.Log ("Globals.POWER = " + Globals.POWER);
			if(Globals.POWER > Globals.BEAST_POWER)
			{
				Globals.POWER = Mathf.Max(0f, Globals.POWER - Globals.BEAST_POWER);
				beast.Die();
			}
		}
	}

	void LateUpdate()
	{
		Color color = VisibleAura.color;
		color.a *= ((Globals.POWER - Globals.BEAST_POWER) / (Globals.MAX_POWER - Globals.BEAST_POWER));
		VisibleAura.color = color;
	}
}
