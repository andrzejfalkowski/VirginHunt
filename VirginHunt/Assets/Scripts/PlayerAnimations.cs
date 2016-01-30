using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

    public Animator animator;
	public Player myPlayer;

	void Awake()
	{
		myPlayer = this.transform.GetComponentInParent<Player>();
	}

    public void AnimationMove()
    {
        animator.SetTrigger("startWalkingAnimation");
    }

    public void AnimationIdle()
    {
        animator.SetTrigger("startIdleAnimation");
    }

    public void AnimationIdleFromPut()
    {
        animator.SetTrigger("startIdleFromPut");
    }

    public void AnimationDie()
    {
        animator.SetTrigger("startDieAnimation");
    }

    public void AnimationTake()
    {
        animator.SetTrigger("startTakeAnimation");
    }

    public void AnimationPut()
    {
        animator.SetTrigger("startPutAnimation");
    }

	public void HandleTakeAnimationFinished()
	{
		myPlayer.HandleTakeAnimationFinished();
	}

	public void HandlePutAnimationFinished()
	{
		myPlayer.HandlePutAnimationFinished();
	}
}
