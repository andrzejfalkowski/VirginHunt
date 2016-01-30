using UnityEngine;
using System.Collections;

public class PrayerSpot : MonoBehaviour {

    public bool IsActiveSpot = false;
    public float Virginity = 1f;

    public void Init()
    {
        Vector3 pos = this.transform.localPosition;
        pos.x = 0f;
        this.transform.localPosition = pos;
    }

    public void AddPower()
    {
        Globals.POWER =+ Virginity * Globals.VIRGINITY_POWER_MOD;
    }

    public void AddCultist(float powerInVillager)
    {
        IsActiveSpot = true;
        Virginity = powerInVillager;
        Debug.Log("Active");
    }

	void OnTriggerEnter2D(Collider2D collider)
	{
		Beast beast = collider.GetComponent<Beast>();
		if(beast != null)
		{
			IsActiveSpot = false;
			beast.Die();
		}
	}
}
