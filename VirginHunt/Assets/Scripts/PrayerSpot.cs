using UnityEngine;
using System.Collections;

public class PrayerSpot : MonoBehaviour {

    public bool IsActiveSpot = false;
    public float Virginity = 1f;
	public Villager ActiveVillager = null;

	public bool ActiveOnStart = false;

	public SpriteRenderer LoadingPower;
	public Transform CultistSpot;

    public void Init()
    {
//        Vector3 pos = this.transform.localPosition;
//        pos.x = 0f;
//        this.transform.localPosition = pos;
		LoadingPower.gameObject.SetActive(false);
		CultistSpot = this.GetComponentInChildren<SpriteRenderer>().transform;

		if(ActiveOnStart)
		{
			Villager cultist = GameController.Instance.SpawnAndReturnVillager();
			AddCultist(cultist);
			cultist.HandleBeingDroppedAsCultist(this);
		}
    }

    public void AddPower()
    {
        Globals.POWER =+ Virginity * Globals.VIRGINITY_POWER_MOD;
    }

    public void AddCultist(Villager newCultist)
    {
		LoadingPower.gameObject.SetActive(true);
        IsActiveSpot = true;
		Virginity = newCultist.Virginity;
		ActiveVillager = newCultist;
        Debug.Log("Active");
    }

	void OnTriggerEnter2D(Collider2D collider)
	{
		Beast beast = collider.GetComponent<Beast>();
		if(beast != null && beast.ReadyToAttack() && IsActiveSpot && ActiveVillager != null)
		{
			ActiveVillager.HandleBeingKilled();
			LoadingPower.gameObject.SetActive(false);
			IsActiveSpot = false;
			ActiveVillager = null;
			beast.Attack();

			GameController.Instance.CheckForAnyCultistsLeft();
		}
	}
}
