using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

    public Animator animator;
	public Player myPlayer;

	public SpriteRenderer LeftHand;
	public SpriteRenderer LeftForearm;

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
		LeftHand.sortingLayerName = "Default";
		LeftForearm.sortingLayerName = "Default";
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
		LeftHand.sortingLayerName = "Player";
		LeftForearm.sortingLayerName = "Player";

		myPlayer.HandlePutAnimationFinished();
	}

    public void AcessToHandleDeathAnimation()
    {
        myPlayer.HandleDeathAnimation();
    }
}
