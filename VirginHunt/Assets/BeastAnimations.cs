using UnityEngine;
using System.Collections;

public class BeastAnimations : MonoBehaviour 
{
	public Beast myBeast;
	
	void Awake()
	{
		myBeast = this.transform.GetComponentInParent<Beast>();
	}

	public void HandleDeathAnimationFinished()
	{
		myBeast.HandleDeathAnimationFinished();
	}

	public void HandleAttackAnimationFinished()
	{
		myBeast.HandleAttackAnimationFinished();
	}
}
