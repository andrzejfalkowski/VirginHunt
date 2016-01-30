using UnityEngine;
using System.Collections;

public class Altar : MonoBehaviour 
{
	public SpriteRenderer PowerGlow;
    public void Init()
    {
        Vector3 pos = this.transform.localPosition;
        pos.x = 0f;
        this.transform.localPosition = pos;
    }
    
    public static void SacrifaceVillager(float powerInVillager)
    {
		Globals.POWER = Mathf.Min(Globals.POWER + powerInVillager, Globals.MAX_POWER);
        Debug.Log("Added POWER! " + powerInVillager);
    }

	void Update()
	{
		Color color = PowerGlow.color;
		color.a = Globals.POWER / Globals.MAX_POWER;
		PowerGlow.color = color;
	}
}
