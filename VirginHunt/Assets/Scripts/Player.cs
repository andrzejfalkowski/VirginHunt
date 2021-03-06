﻿using UnityEngine;
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
		Dropping,
		Dying
	}
	public EPlayerState CurrentState = EPlayerState.Idle;
	private EPlayerState previousState = EPlayerState.Idle;

	public bool IsCarryingVillager = false;
    public bool IsOnAltar = true;
    public bool IsOnPrayerSpot = false;
	public Villager CarriedVillager = null;
	public Transform PickablePoint;

    private bool facingLeft = false;

    public PrayerSpot currentPrayerSpot = null;
    public PlayerAnimations playerAnimations;

	[SerializeField]
	private List<Villager> collidingVillagers = new List<Villager>();

	public void Init() 
	{
		collidingVillagers.Clear();

		Vector3 pos = this.transform.localPosition;
		pos.y = 0f;
		this.transform.localPosition = pos;
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
            case EPlayerState.Idle:
                
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
            TurnLeft();
            playerAnimations.AnimationMove();         
        }
	}

	public void MoveRight()
	{
		if(CurrentState == EPlayerState.Idle || CurrentState == EPlayerState.WalkingLeft)
		{
			CurrentState = EPlayerState.WalkingRight;
            TurnRight();
            playerAnimations.AnimationMove();
        }
	}

	public void StopMoving()
	{
		if(CurrentState == EPlayerState.WalkingLeft || CurrentState == EPlayerState.WalkingRight)
		{
			CurrentState = EPlayerState.Idle;
            playerAnimations.AnimationIdle();
        }
	}

	public void HandleSpaceAction()
	{
		if(CurrentState == EPlayerState.Dropping || CurrentState == EPlayerState.Dying || CurrentState == EPlayerState.PickingUp)
			return;

		if(IsCarryingVillager)
		{
			CurrentState = EPlayerState.Dropping;
            if(IsOnAltar)
            {
				DropVillagerAsSacrifice();
            }
            else if(IsOnPrayerSpot && !currentPrayerSpot.IsActiveSpot)
            {
				DropVillagerAsCultist();
            }
            else
            {
				DropVillager();
            }
        }
		else
		{
			if(collidingVillagers.Count > 0)
			{
				IsCarryingVillager = true;
                playerAnimations.AnimationTake();
				CurrentState = EPlayerState.PickingUp;
				CarriedVillager = collidingVillagers[0];
				CarriedVillager.HandleBeingPickedUp();

				GetComponent<AudioSource>().Play();
			}
		}
	}

	void DropVillagerAsSacrifice()
	{
		playerAnimations.AnimationSacrifice();
		Altar.SacrifaceVillager(CarriedVillager.IsVirgin ? Globals.VIRGIN_SACRIFICE_BONUS : Globals.NONVIRGIN_SACRIFICE_BONUS, CarriedVillager.IsVirgin);
		CarriedVillager.HandleBeingDroppedAsSacrifice();
		IsCarryingVillager = false;

		if(collidingVillagers.Count > 0)
			collidingVillagers[collidingVillagers.Count - 1].GetComponent<Flash>().HideFlash();
	}

	void DropVillagerAsCultist()
	{
		playerAnimations.AnimationPut();
		currentPrayerSpot.AddCultist(CarriedVillager);
		CarriedVillager.HandleBeingDroppedAsCultist(currentPrayerSpot);
		IsCarryingVillager = false;

		if(collidingVillagers.Count > 0)
			collidingVillagers[collidingVillagers.Count - 1].GetComponent<Flash>().HideFlash();
	}

	void DropVillager(bool animated = true)
	{
		if(animated)
			playerAnimations.AnimationPut();
		IsCarryingVillager = false;

		if(collidingVillagers.Count > 0)
			collidingVillagers[collidingVillagers.Count - 1].GetComponent<Flash>().HideFlash();

		if(CarriedVillager != null)
		{
			CarriedVillager.HandleBeingDropped();
			CarriedVillager = null;
		}
	}

	void Die()
	{
		CurrentState = EPlayerState.Dying;
		playerAnimations.AnimationDie();
		DropVillager(false);
	}

	public void HandleDeathAnimation()
	{
		GameController.Instance.GameOver(false);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(CurrentState == EPlayerState.Dying)
			return;

		Villager villager = collider.GetComponent<Villager>();
        Altar altar = collider.GetComponent<Altar>();
        PrayerSpot prayerSpot = collider.GetComponent<PrayerSpot>();
		Beast beast = collider.GetComponent<Beast>();
		if(villager != null && !collidingVillagers.Contains(villager) && villager.CanBePickedUp())
		{
			villager.ShowThought();
			if(collidingVillagers.Count > 0)
				collidingVillagers[collidingVillagers.Count - 1].GetComponent<Flash>().HideFlash();

			collidingVillagers.Add(villager);
			if(!IsCarryingVillager)
				villager.GetComponent<Flash>().ShowFlash();

		}
        else if(altar != null)
        {
            IsOnAltar = true;
			altar.AltarSprite.transform.GetComponent<Flash>().ShowFlash();
        }
        else if (prayerSpot != null)
        {
			if(currentPrayerSpot != null)
				currentPrayerSpot.GetComponent<Flash>().HideFlash();
            IsOnPrayerSpot = true;
            currentPrayerSpot = prayerSpot;
			currentPrayerSpot.GetComponent<Flash>().ShowFlash();
        }
		else if(beast != null && beast.ReadyToAttack())
		{
			beast.Attack();
			Die();
			//GameController.Instance.GameOver();
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		Villager villager = collider.GetComponent<Villager>();
        Altar altar = collider.GetComponent<Altar>();
        PrayerSpot prayerSpot = collider.GetComponent<PrayerSpot>();
        if (villager != null && collidingVillagers.Contains(villager))
		{
			villager.HideThought();
			villager.GetComponent<Flash>().HideFlash();
			collidingVillagers.Remove(villager);
		}
        else if(altar != null)
        {
            IsOnAltar = false;
			altar.AltarSprite.transform.GetComponent<Flash>().HideFlash();
        }
        else if(prayerSpot != false)
        {
            IsOnPrayerSpot = false;
			currentPrayerSpot.GetComponent<Flash>().HideFlash();
            currentPrayerSpot = null;
        }
	}

	public void SetNewXPosition(float newX)
	{
		Vector3 pos = this.transform.localPosition;
		pos.x = Mathf.Min(Mathf.Max(Globals.MAP_MIN_X, newX), Globals.MAP_MAX_X);
		this.transform.localPosition = pos;

		GameController.Instance.MainCamera.GetComponent<MainCamera>().SetNewXPosition(pos.x);
	}

    public void TurnLeft()
    {
        if (!facingLeft)
        {
            Vector3 newScale = this.transform.localScale;
            newScale.x *= -1;
            this.transform.localScale = newScale;
            facingLeft = true;
        }
    }

    public void TurnRight()
    {
        if (facingLeft)
        {
            Vector3 newScale = this.transform.localScale;
            newScale.x *= -1;
            this.transform.localScale = newScale;
            facingLeft = false;
        }
    }

	public void HandleTakeAnimationFinished()
	{
		CurrentState = EPlayerState.Idle;
        playerAnimations.AnimationIdle();
    }

	public void HandlePutAnimationFinished()
	{
		CurrentState = EPlayerState.Idle;
        playerAnimations.AnimationIdle();
    }

	public void RemoveVillagerFromColliding(Villager villager)
	{
		villager.HideThought();

		if (collidingVillagers.Contains(villager))
		{
			collidingVillagers.Remove(villager);
			villager.GetComponent<Flash>().HideFlash();
		}
	}
}
