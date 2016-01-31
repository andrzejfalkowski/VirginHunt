using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Altar : MonoBehaviour 
{
	public SpriteRenderer PowerGlow;
	public List<Sprite> GlowSprites;
	public Transform SacrificeSpot;
	float currentLevel = 0f;

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
		currentLevel = ((Globals.START_POWER / Globals.MAX_POWER) * (GlowSprites.Count - 1));

		PrayerSpot[] prayerSpots = GetComponentsInChildren<PrayerSpot>();
		foreach(PrayerSpot prayerSpot in prayerSpots)
		{
			prayerSpot.Init();
		}
    }
    
    public static void SacrifaceVillager(float powerInVillager)
    {
		Globals.POWER = Mathf.Min(Globals.POWER + powerInVillager, Globals.MAX_POWER);
        Debug.Log("Added POWER! " + powerInVillager);
    }

	void Update()
	{
		float targetLevel = ((Globals.POWER / Globals.MAX_POWER) * (GlowSprites.Count - 1));
		float newLevel = currentLevel;
		if(currentLevel > targetLevel)
			newLevel = Mathf.Max(newLevel - Time.deltaTime * 3f, targetLevel);
		else if(currentLevel < targetLevel)
			newLevel =  Mathf.Min(newLevel + Time.deltaTime * 3f, targetLevel);
		PowerGlow.sprite = GlowSprites[(int)newLevel];
		currentLevel = newLevel;

//		Debug.Log ((int)newLevel);

//		Color color = PowerGlow.color;
//		color.a = Globals.POWER / Globals.MAX_POWER;
//		PowerGlow.color = color;
	}
}
