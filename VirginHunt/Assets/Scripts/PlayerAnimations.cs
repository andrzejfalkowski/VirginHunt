using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

    public Animator animator;

    public void AnimationMove()
    {
        animator.SetTrigger("startWalkingAnimation");
    }

    public void AnimationIdle()
    {
        animator.SetTrigger("startIdleAnimation");
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
        AnimationIdle();
    }
}
