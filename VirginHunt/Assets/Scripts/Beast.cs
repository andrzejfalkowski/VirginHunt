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

	public void Init()
	{
		IsFacingLeft = Random.Range(0, 2) > 0;
		float x = GameController.Instance.MainCamera.ViewportToWorldPoint(new Vector3((IsFacingLeft ? 1f: 0f), 0f, 0f)).x;

		if(IsFacingLeft)
			CurrentState = EBeastState.RunningLeft;
		else
			CurrentState = EBeastState.RunningRight;

		Vector3 pos = this.transform.localPosition;
		pos.x = x;
		this.transform.localPosition = pos;
	}

    void Update()
    {
        switch (CurrentState)
        {
            case EBeastState.RunningLeft:
                SetNewXPosition(this.transform.localPosition.x - (Globals.BEAST_MOVEMENT_SPEED * Time.deltaTime));
                //RUNNING LEFT Animation
                break;
            case EBeastState.RunningRight:
                SetNewXPosition(this.transform.localPosition.x + (Globals.BEAST_MOVEMENT_SPEED * Time.deltaTime));
                //RUNNING RIGHT Animation
                break;
            case EBeastState.EatingCultist:
                if (actualEatingTime > timeToFinishEating)
                {
                    //Fadeout
                    HandleDeathAnimationFinished();
                }
                else
                {
                    actualEatingTime += Time.deltaTime;
                    //EATING Animation
                }
                break;
        }
    }

	public void Die()
	{
		CurrentState = EBeastState.Dying;
	}

	public void HandleDeathAnimationFinished()
	{
		Destroy(this.gameObject);
	}

    public void SetNewXPosition(float newX)
    {
        Vector3 pos = this.transform.localPosition;
        pos.x = newX;
        this.transform.localPosition = pos;
    }

}
