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

			}
		}
	}

	void DropVillagerAsSacrifice()
	{
		playerAnimations.AnimationPut();
		Altar.SacrifaceVillager(CarriedVillager.Virginity * Globals.VIRGINITY_POWER_MOD);
		CarriedVillager.HandleBeingDroppedAsSacrifice();
		IsCarryingVillager = false;
	}

	void DropVillagerAsCultist()
	{
		playerAnimations.AnimationPut();
		currentPrayerSpot.AddCultist(CarriedVillager);
		CarriedVillager.HandleBeingDroppedAsCultist(currentPrayerSpot);
		IsCarryingVillager = false;
	}

	void DropVillager(bool animated = true)
	{
		if(animated)
			playerAnimations.AnimationPut();
		IsCarryingVillager = false;
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

	void HandleDeathAnimation()
	{
		GameController.Instance.GameOver();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Villager villager = collider.GetComponent<Villager>();
        Altar altar = collider.GetComponent<Altar>();
        PrayerSpot prayerSpot = collider.GetComponent<PrayerSpot>();
		Beast beast = collider.GetComponent<Beast>();
		if(villager != null && !collidingVillagers.Contains(villager) && villager.CanBePickedUp())
		{
			collidingVillagers.Add(villager);
		}
        else if(altar != null)
        {
            IsOnAltar = true;
        }
        else if (prayerSpot != null)
        {
            IsOnPrayerSpot = true;
            currentPrayerSpot = prayerSpot;
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
			collidingVillagers.Remove(villager);
		}
        else if(altar != null)
        {
            IsOnAltar = false;
        }
        else if(prayerSpot != false)
        {
            IsOnPrayerSpot = false;
            currentPrayerSpot = null;
        }
	}

	public void SetNewXPosition(float newX)
	{
		Vector3 pos = this.transform.localPosition;
		pos.x = newX;
		this.transform.localPosition = pos;

		GameController.Instance.MainCamera.GetComponent<MainCamera>().SetNewXPosition(newX);
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
}
