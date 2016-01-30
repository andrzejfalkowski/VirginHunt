using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour 
{
	public enum EVillagerState
	{
		Idle,
		WalkingLeft,
		WalkingRight,
		PickedUp
	}
	public EVillagerState CurrentState = EVillagerState.Idle;

    private float movementRandomSeed = 0f;
    private float timeToChangeVillagerMovement = 0f;
    private float maximumTimeToChangeVillagerMovement = 5f;

    private float minimumRollForRandomSeed = 0f;
    private float maximumRollForRandomSeed = 3f;

	public void Init()
	{
		Vector3 pos = this.transform.localPosition;
		pos.x = Random.Range (Globals.VILLAGERS_MIN_X, Globals.VILLAGERS_MAX_X);
		this.transform.localPosition = pos;

        ChooseVillagerMovementDirection();
		maximumTimeToChangeVillagerMovement = Random.Range(3f, 6f);
    }

	void Update () 
	{   
        switch (CurrentState)
        {
            case EVillagerState.Idle:
                if (timeToChangeVillagerMovement > maximumTimeToChangeVillagerMovement)
                {
                    timeToChangeVillagerMovement = 0f;
                    ChooseVillagerMovementDirection();
                }
                else
                {
                    timeToChangeVillagerMovement += Time.deltaTime;
                    //IDLE Animation
                }
            break;
            case EVillagerState.WalkingLeft:
                if (timeToChangeVillagerMovement > maximumTimeToChangeVillagerMovement)
                {
                    timeToChangeVillagerMovement = 0f;
                    ChooseVillagerMovementDirection();
                }
                else
                {
                    timeToChangeVillagerMovement += Time.deltaTime;
                    SetNewXPosition(this.transform.localPosition.x - (Globals.VILLAGER_MOVEMENT_SPEED * Time.deltaTime));
                    //WALKING LEFT Animation
                }
            break;
            case EVillagerState.WalkingRight:
                if (timeToChangeVillagerMovement > maximumTimeToChangeVillagerMovement)
                {
                    timeToChangeVillagerMovement = 0f;
                    ChooseVillagerMovementDirection();
                }
                else
                {
                    timeToChangeVillagerMovement += Time.deltaTime;
                    SetNewXPosition(this.transform.localPosition.x + (Globals.VILLAGER_MOVEMENT_SPEED * Time.deltaTime));
                    //WALKING RIGHT Animation
                }
            break;
            case EVillagerState.PickedUp:
            break;
        }
	}

    void ChooseVillagerMovementDirection()
    {
        movementRandomSeed = Random.Range(0f, maximumRollForRandomSeed);
        if (movementRandomSeed <= 2f)
        {
            CurrentState = EVillagerState.Idle;
        }
        else if (movementRandomSeed <= 2.5f)
        {
            CurrentState = EVillagerState.WalkingLeft;
        }
        else
        {
            CurrentState = EVillagerState.WalkingRight;
        }
    }

	public void HandleBeingPickedUp()
	{
		CurrentState = EVillagerState.PickedUp;
		this.transform.SetParent(GameController.Instance.PlayerCharacter.PickablePoint);
		this.transform.localPosition = Vector3.zero;
	}

	public void HandleBeingDropped()
	{
		CurrentState = EVillagerState.Idle;
		this.transform.SetParent(GameController.Instance.PlayerCharacter.transform.parent);
		this.transform.localPosition = GameController.Instance.PlayerCharacter.transform.localPosition;
	}

    public void HandleBeingKilled()
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
