using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public enum EPlayerState
	{
		Idle,
		WalkingLeft,
		WalkingRight,
		PickingUp,
		Dropping
	}
	public EPlayerState CurrentState = EPlayerState.Idle;
	private EPlayerState previousState = EPlayerState.Idle;

	public bool IsCarryingVillager = false;
	
	public void Init() 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(CurrentState)
		{
			case EPlayerState.WalkingLeft:
				SetNewXPosition(this.transform.localPosition.x - 
			                (IsCarryingVillager ? Globals.PLAYER_CARRY_SPEED * Time.deltaTime : Globals.PLAYER_WALK_SPEED * Time.deltaTime));
				break;
			case EPlayerState.WalkingRight:
				SetNewXPosition(this.transform.localPosition.x + 
			                (IsCarryingVillager ? Globals.PLAYER_CARRY_SPEED * Time.deltaTime : Globals.PLAYER_WALK_SPEED * Time.deltaTime));
				break;
		}
		// TODO; animations

		previousState = CurrentState;
	}

	public void MoveLeft()
	{
		if(CurrentState == EPlayerState.Idle)
		{
			CurrentState = EPlayerState.WalkingLeft;
		}
	}

	public void MoveRight()
	{
		if(CurrentState == EPlayerState.Idle)
		{
			CurrentState = EPlayerState.WalkingRight;
		}
	}

	public void StopMoving()
	{
		if(CurrentState == EPlayerState.WalkingLeft || CurrentState == EPlayerState.WalkingRight)
		{
			CurrentState = EPlayerState.Idle;
		}
	}

	public void HandleSpaceAction()
	{

		if(IsCarryingVillager)
		{
			// TODO
		}
		else
		{
			// TODO
		}
	}

	public void SetNewXPosition(float newX)
	{
		Vector3 pos = this.transform.localPosition;
		pos.x = newX;
		this.transform.localPosition = pos;

		GameController.Instance.MainCamera.GetComponent<MainCamera>().SetNewXPosition(newX);
	}
}
