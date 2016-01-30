using UnityEngine;
using System.Collections;

public class Altar : MonoBehaviour {

    public void Init()
    {
        Vector3 pos = this.transform.localPosition;
        pos.x = 0f;
        this.transform.localPosition = pos;
    }
    
    public static void SacrifaceVillager(float powerInVillager)
    {
        Globals.POWER += powerInVillager;
        Debug.Log("Added POWER! " + powerInVillager);
    }
}
