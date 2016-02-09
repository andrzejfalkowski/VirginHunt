using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoodController : MonoBehaviour 
{
	public float Modifier = 1f;
	SpriteRenderer[] sprites;

	void Start () 
	{
		sprites = GetComponentsInChildren<SpriteRenderer>();
	}

	void Update()
	{
		float power = 0f;
		if(GameController.Instance.CurrentGamePhase == GameController.EGamePhase.Day)
		{
			power = 1f - Mathf.Abs((Globals.DAY_DURATION / 2f) - GameController.Instance.CurrentPhaseTime) / (Globals.DAY_DURATION / 2f);
		}
		else
			power = 0f;

		power = power * Modifier + (1 - Modifier);
		//Debug.Log(power);
		foreach(var sprite in sprites)
		{
			sprite.color = new Color(power + 0.05f, power, power + 0.1f, 1f);
		}
		
	}
}
