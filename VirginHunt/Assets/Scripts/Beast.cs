using UnityEngine;
using System.Collections;

public class Beast : MonoBehaviour 
{
	public enum EBeastState
	{
		RunningLeft,
		RunningRight,
		EatingCultist,
		Dying
	}
	public EBeastState CurrentState = EBeastState.RunningLeft;
	public bool IsCarryingCultist = false;
    public bool IsFacingLeft = true;

    private float actualEatingTime = 0f;
    private float timeToFinishEating = 2f;

	private Animator myAnimator;

	public void Init()
	{
		myAnimator = GetComponentInChildren<Animator>();

		IsFacingLeft = Random.Range(0, 2) > 0;
		float x = IsFacingLeft ? Globals.MAP_MAX_X : Globals.MAP_MIN_X;
			//GameController.Instance.MainCamera.ViewportToWorldPoint(new Vector3((IsFacingLeft ? 1f: 0f), 0f, 0f)).x;

		if(IsFacingLeft)
		{
			CurrentState = EBeastState.RunningLeft;

			Vector3 newScale = this.transform.localScale;
			newScale.x *= -1;
			this.transform.localScale = newScale;
		}
		else
		{
			CurrentState = EBeastState.RunningRight;
		}

		Vector3 pos = this.transform.localPosition;
		pos.x = x;
		pos.y = 0f;
		this.transform.localPosition = pos;
	}

    void Update()
    {
        switch (CurrentState)
        {
            case EBeastState.RunningLeft:
                SetNewXPosition(this.transform.localPosition.x - (Globals.BEAST_MOVEMENT_SPEED * Time.deltaTime));
				myAnimator.Play("move");
                //RUNNING LEFT Animation
                break;
            case EBeastState.RunningRight:
                SetNewXPosition(this.transform.localPosition.x + (Globals.BEAST_MOVEMENT_SPEED * Time.deltaTime));
				myAnimator.Play("move");
                //RUNNING RIGHT Animation
                break;
            case EBeastState.EatingCultist:
//                if (actualEatingTime > timeToFinishEating)
//                {
//                    //Fadeout
//                    HandleDeathAnimationFinished();
//                }
//                else
//                {
//                    actualEatingTime += Time.deltaTime;
//                    //EATING Animation
//                }
                break;
        }
    }

	public void Die()
	{
		if(myAnimator != null)
			myAnimator.Play("death");
		CurrentState = EBeastState.Dying;
	}

	public bool ReadyToAttack()
	{
		return (CurrentState == EBeastState.RunningLeft || CurrentState == EBeastState.RunningRight);
	}

	public void Attack()
	{
		if(myAnimator != null)
			myAnimator.Play("attack");
		CurrentState = EBeastState.EatingCultist;
	}

	public void HandleDeathAnimationFinished()
	{
		Destroy(this.gameObject);
	}

	public void HandleAttackAnimationFinished()
	{
		Die();
	}

    public void SetNewXPosition(float newX)
    {
        Vector3 pos = this.transform.localPosition;
        pos.x = newX;
        this.transform.localPosition = pos;
    }

}
