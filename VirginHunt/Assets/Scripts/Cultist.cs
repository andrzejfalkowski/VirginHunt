using UnityEngine;
using System.Collections;

public class Cultist : MonoBehaviour 
{
	public bool Dying = false;
	public void Die()
	{
		if(!Dying)
		{
			Dying = true;
			// TODO: call animation
		}
	}
	
	public void HandleDeathAnimationFinished()
	{
		Destroy(this.gameObject);
	}
}
