using UnityEngine;
using System.Collections;

public class VillagerAnimations : MonoBehaviour 
{
	public Animator animator;
	public Villager myVillager;
	
	void Awake()
	{
		myVillager = this.transform.GetComponentInParent<Villager>();
	}
	
	public void AnimationMove()
	{
		animator.SetBool("Carried", false);
		animator.SetBool("Idle", false);
		animator.SetBool("Walking", true);
	}

	public void AnimationIdle()
	{
		animator.SetBool("Carried", false);
		animator.SetBool("Walking", false);
		animator.SetBool("Idle", true);
	}

	public void AnimationCarried()
	{
		animator.SetBool("Carried", true);
		animator.SetBool("Walking", false);
		animator.SetBool("Idle", false);
	}
}
