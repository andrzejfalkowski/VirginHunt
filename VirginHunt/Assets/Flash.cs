using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour 
{
	[SerializeField]
	private bool parentOnly = false;
	private SpriteRenderer[] sprites;
	// Use this for initialization
	void Start () 
	{
		if(parentOnly)
			sprites = this.GetComponents<SpriteRenderer>();
		else
			sprites = this.GetComponentsInChildren<SpriteRenderer>();
		HideFlash();
	}
	
	// Update is called once per frame
	public void ShowFlash() 
	{
		for(int i = 0; i < sprites.Length; i++)
		{
			sprites[i].material.SetFloat("_FlashAmount", 0.2f);
		}
	}

	public void HideFlash() 
	{
		for(int i = 0; i < sprites.Length; i++)
		{
			sprites[i].material.SetFloat("_FlashAmount", 0f);
		}
	}
}
