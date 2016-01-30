using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Altar : MonoBehaviour 
{
	public SpriteRenderer PowerGlow;
	public List<Sprite> GlowSprites;

    public void Init()
    {
        Vector3 pos = this.transform.localPosition;
        pos.x = 0f;
		pos.y = 3.2f;
        this.transform.localPosition = pos;

		GlowSprites.Sort(
			delegate(Sprite i1, Sprite i2) 
			{ 
				return i1.name.CompareTo(i2.name); 
			}
		);
    }
    
    public static void SacrifaceVillager(float powerInVillager)
    {
		Globals.POWER = Mathf.Min(Globals.POWER + powerInVillager, Globals.MAX_POWER);
        Debug.Log("Added POWER! " + powerInVillager);
    }

	void Update()
	{
		PowerGlow.sprite = GlowSprites[(int)((Globals.POWER / Globals.MAX_POWER) * (GlowSprites.Count - 1))];
//		Color color = PowerGlow.color;
//		color.a = Globals.POWER / Globals.MAX_POWER;
//		PowerGlow.color = color;
	}
}
