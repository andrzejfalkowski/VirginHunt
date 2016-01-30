using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Shirt
{
	public Sprite Torso;
	public Sprite Arm;
}

[System.Serializable]
public class Pants
{
	public Sprite LeftLeg;
	public Sprite RightLeg;
}

[System.Serializable]
public class Shoes
{
	public Sprite LeftShoe;
	public Sprite RightShoe;
}

public class Villager : MonoBehaviour 
{
	public enum EVillagerState
	{
		Idle,
		WalkingLeft,
		WalkingRight,
		PickedUp,
		Praying,
		Dying
	}
	public EVillagerState CurrentState = EVillagerState.Idle;

	public float Virginity = 1f;

	public bool Woman = false;

	public List<Sprite> MaleHatSprites;
	public List<Sprite> FemaleHatSprites;

	public List<Sprite> MaleHeadSprites;
	public List<Sprite> FemaleHeadSprites;

	public List<Shirt> MaleShirtSprites;
	public List<Shirt> FemaleShirtSprites;

	public List<Pants> MalePantsSprites;
	public List<Pants> FemalePantsSprites;

	public List<Shoes> MaleShoesSprites;
	public List<Shoes> FemaleShoesSprites;

	public SpriteRenderer Hat;
	public SpriteRenderer Head;
	public SpriteRenderer Torso;
	public SpriteRenderer LeftArm;
	public SpriteRenderer RightArm;
	public SpriteRenderer LeftLeg;
	public SpriteRenderer RightLeg;
	public SpriteRenderer LeftShoe;
	public SpriteRenderer RightShoe;

	public VillagerAnimations villagerAnimations;

    private float movementRandomSeed = 0f;
    private float timeToChangeVillagerMovement = 0f;
    private float maximumTimeToChangeVillagerMovement = 5f;

    private float minimumRollForRandomSeed = 0f;
    private float maximumRollForRandomSeed = 3f;

	public void Init()
	{
		Vector3 pos = this.transform.localPosition;
		pos.x = Random.Range (Globals.VILLAGERS_MIN_X, Globals.VILLAGERS_MAX_X);
		pos.y = 0f;
		this.transform.localPosition = pos;

        ChooseVillagerMovementDirection();
		maximumTimeToChangeVillagerMovement = Random.Range(3f, 6f);

		Virginity = Random.Range(0f, 1f);

		Woman = Random.Range(0, 2) > 0;

		if (Woman)
			Hat.sprite = FemaleHatSprites[Random.Range(0, FemaleHatSprites.Count - 1)];
		else
			Hat.sprite = MaleHatSprites[Random.Range(0, FemaleHatSprites.Count - 1)];

		if (Woman)
			Head.sprite = FemaleHeadSprites[Random.Range(0, FemaleHeadSprites.Count - 1)];
		else
			Head.sprite = MaleHeadSprites[Random.Range(0, FemaleHeadSprites.Count - 1)];


		if (Woman)
		{
			int random = Random.Range(0, FemaleShirtSprites.Count - 1);
			Torso.sprite = FemaleShirtSprites[random].Torso;
			LeftArm.sprite = FemaleShirtSprites[random].Arm;
			RightArm.sprite = FemaleShirtSprites[random].Arm;
		}
		else
		{
			int random = Random.Range(0, MaleShirtSprites.Count - 1);
			Torso.sprite = MaleShirtSprites[random].Torso;
			LeftArm.sprite = MaleShirtSprites[random].Arm;
			RightArm.sprite = MaleShirtSprites[random].Arm;
		}

		if (Woman)
		{
			int random = Random.Range(0, FemalePantsSprites.Count - 1);
			LeftLeg.sprite = FemalePantsSprites[random].LeftLeg;
			RightLeg.sprite = FemalePantsSprites[random].RightLeg;
		}
		else
		{
			int random = Random.Range(0, MalePantsSprites.Count - 1);
			LeftLeg.sprite = MalePantsSprites[random].LeftLeg;
			RightLeg.sprite = MalePantsSprites[random].RightLeg;
		}

		if (Woman)
		{
			int random = Random.Range(0, FemaleShoesSprites.Count - 1);
			LeftShoe.sprite = FemaleShoesSprites[random].LeftShoe;
			RightShoe.sprite = FemaleShoesSprites[random].RightShoe;
		}
		else
		{
			int random = Random.Range(0, MaleShoesSprites.Count - 1);
			LeftShoe.sprite = MaleShoesSprites[random].LeftShoe;
			RightShoe.sprite = MaleShoesSprites[random].RightShoe;
		}
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
			villagerAnimations.AnimationIdle();
        }
        else if (movementRandomSeed <= 2.5f)
        {
            CurrentState = EVillagerState.WalkingLeft;
			Vector3 newScale = this.transform.localScale;
			newScale.x = -1f;
			this.transform.localScale = newScale;
			villagerAnimations.AnimationMove();
        }
        else
        {
            CurrentState = EVillagerState.WalkingRight;
			Vector3 newScale = this.transform.localScale;
			newScale.x = 1f;
			this.transform.localScale = newScale;
			villagerAnimations.AnimationMove();
        }
    }

	public bool CanBePickedUp()
	{
		return (CurrentState != EVillagerState.Dying && CurrentState != EVillagerState.PickedUp && CurrentState != EVillagerState.Praying);
	}

	public void HandleBeingPickedUp()
	{
		CurrentState = EVillagerState.PickedUp;
		this.transform.SetParent(GameController.Instance.PlayerCharacter.PickablePoint);
		this.transform.localPosition = Vector3.zero;
		villagerAnimations.AnimationCarried();
	}

	public void HandleBeingDropped()
	{
		CurrentState = EVillagerState.Idle;
		this.transform.SetParent(GameController.Instance.PlayerCharacter.transform.parent);
		this.transform.localPosition = GameController.Instance.PlayerCharacter.transform.localPosition;
		villagerAnimations.AnimationIdle();
	}

	public void HandleBeingDroppedAsCultist(PrayerSpot prayerSpot)
	{
		CurrentState = EVillagerState.Praying;
		this.transform.SetParent(prayerSpot.transform);
		this.transform.localPosition = prayerSpot.CultistSpot.localPosition;
		villagerAnimations.AnimationPray();
	}

    public void HandleBeingKilled()
    {
		CurrentState = EVillagerState.Dying;
		villagerAnimations.AnimationDie();
    }

	public void HandleDieAnimationFinished()
	{
		Destroy(this.gameObject);
	}

    public void SetNewXPosition(float newX)
    {
        Vector3 pos = this.transform.localPosition;
        pos.x = newX;
        this.transform.localPosition = pos;
    }

    void OnDestroy()
    {
        if (GameController.Instance.Villagers.Contains(this))
        {
            GameController.Instance.Villagers.Remove(this);
        }
    }
}
