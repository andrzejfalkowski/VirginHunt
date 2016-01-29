using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour 
{
	public enum EVillagerState
	{
		Idle,
		WalkingLeft,
		WalkingRight,
		PickedUp
	}
	public EVillagerState CurrentState = EVillagerState.Idle;

	public void Init()
	{
		Vector3 pos = this.transform.localPosition;
		pos.x = Random.Range (Globals.VILLAGERS_MIN_X, Globals.VILLAGERS_MAX_X);
		this.transform.localPosition = pos;
	}

	void Update () 
	{
	
	}
}
