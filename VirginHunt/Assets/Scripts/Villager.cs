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

	public void Init()
	{
		Vector3 pos = this.transform.localPosition;
		pos.x = Random.Range (Globals.VILLAGERS_MIN_X, Globals.VILLAGERS_MAX_X);
		this.transform.localPosition = pos;

        ChooseVillagerMovementDirection();
    }

	void Update () 
	{   
        switch (CurrentState)
        {
            case EVillagerState.Idle:
                if (timeToChangeVillagerMovement > 3f)
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
                if (timeToChangeVillagerMovement > 3f)
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
                if (timeToChangeVillagerMovement > 3f)
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
        movementRandomSeed = Random.Range(0f, 3f);
        if (movementRandomSeed <= 1)
        {
            CurrentState = EVillagerState.Idle;
        }
        else if (movementRandomSeed <= 2)
        {
            CurrentState = EVillagerState.WalkingLeft;
        }
        else
        {
            CurrentState = EVillagerState.WalkingRight;
        }
    }

    public void SetNewXPosition(float newX)
    {
        Vector3 pos = this.transform.localPosition;
        pos.x = newX;
        this.transform.localPosition = pos;
    }
}
