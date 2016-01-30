using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public bool IsOnAltar = true;
	public Villager CarriedVillager = null;
	public Transform PickablePoint;

	[SerializeField]
	private List<Villager> collidingVillagers = new List<Villager>();

	public void Init() 
	{
		collidingVillagers.Clear();
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
		if(CurrentState == EPlayerState.Idle || CurrentState == EPlayerState.WalkingRight)
		{
			CurrentState = EPlayerState.WalkingLeft;
		}
	}

	public void MoveRight()
	{
		if(CurrentState == EPlayerState.Idle || CurrentState == EPlayerState.WalkingLeft)
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
            if(IsOnAltar)
            {
				Altar.SacrifaceVillager(CarriedVillager.Virginity * Globals.VIRGINITY_POWER_MOD);
                CarriedVillager.HandleBeingKilled();
                IsCarryingVillager = false;
            }
            else
            {     
			    IsCarryingVillager = false;
			    CarriedVillager.HandleBeingDropped();
			    CarriedVillager = null;
            }
        }
		else
		{
			if(collidingVillagers.Count > 0)
			{
				IsCarryingVillager = true;
				CarriedVillager = collidingVillagers[0];
				CarriedVillager.HandleBeingPickedUp();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Villager villager = collider.GetComponent<Villager>();
        Altar altar = collider.GetComponent<Altar>();
		if(villager != null && !collidingVillagers.Contains(villager))
		{
			collidingVillagers.Add(villager);
		}
        else if(altar != null)
        {
            IsOnAltar = true;
        }
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		Villager villager = collider.GetComponent<Villager>();
        Altar altar = collider.GetComponent<Altar>();
		if(villager != null && collidingVillagers.Contains(villager))
		{
			collidingVillagers.Remove(villager);
		}
        else if(altar != null)
        {
            IsOnAltar = false;
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
