using UnityEngine;
using System.Collections;

public class StartAnimationWithDelay : MonoBehaviour 
{
	public Animator DelayedAnimator;
	public float Delay = 0f;
	float timer = 0f;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
		if(timer > Delay)
		{
			DelayedAnimator.enabled = true;
			Destroy(this);
		}
	}
}
