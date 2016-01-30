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
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", true);
    }

    public void AnimationIdle()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("Idle", true);
    }

    public void AnimationDie()
    {
        animator.SetBool("Die", true);
    }

    public void AnimationTake()
    {
        animator.SetBool("PickedUp", true);
    }

    public void AnimationPut()
    {
        animator.SetBool("PickedUp", false);
    }

	public void HandleTakeAnimationFinished()
	{
		myPlayer.HandleTakeAnimationFinished();
	}

	public void HandlePutAnimationFinished()
	{
		myPlayer.HandlePutAnimationFinished();
	}

    public void AcessToHandleDeathAnimation()
    {
        myPlayer.HandleDeathAnimation();
    }
}
