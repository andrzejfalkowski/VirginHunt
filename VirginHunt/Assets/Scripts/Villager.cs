using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

[System.Serializable]
public class Thought
{
	public Sprite Icon;
	public float VirginityLevel;
}

[System.Serializable]
public class Hat
{
	public Sprite HatSprite;
	public float VirginityLevel;
}

[System.Serializable]
public class Head
{
	public Sprite HeadSprite;
	public float VirginityLevel;
}

[System.Serializable]
public class Shirt
{
	public Sprite Torso;
	public Sprite Arm;
	public float VirginityLevel;
}

[System.Serializable]
public class Pants
{
	public Sprite LeftLeg;
	public Sprite RightLeg;
	public float VirginityLevel;
}

[System.Serializable]
public class Shoes
{
	public Sprite LeftShoe;
	public Sprite RightShoe;
	public float VirginityLevel;
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

	public bool IsVirgin;
	public float Virginity = 1f;

	public bool Woman = false;

	public List<Thought> MaleThoughtSprites;
	public List<Thought> FemaleThoughtSprites;

	public List<Hat> MaleHatSprites;
	public List<Hat> FemaleHatSprites;

	public List<Head> MaleHeadSprites;
	public List<Head> FemaleHeadSprites;

	public List<Shirt> MaleShirtSprites;
	public List<Shirt> FemaleShirtSprites;

	public List<Pants> MalePantsSprites;
	public List<Pants> FemalePantsSprites;

	public SpriteRenderer ThoughtBubble;
	public SpriteRenderer ThoughtIcon;
	public SpriteRenderer Hat;
	public SpriteRenderer Head;
	public SpriteRenderer Torso;
	public SpriteRenderer LeftArm;
	public SpriteRenderer RightArm;
	public SpriteRenderer LeftLeg;
	public SpriteRenderer RightLeg;

	public VillagerAnimations villagerAnimations;

    private float movementRandomSeed = 0f;
    private float timeToChangeVillagerMovement = 0f;
    private float maximumTimeToChangeVillagerMovement = 5f;

    private float minimumRollForRandomSeed = 0f;
    private float maximumRollForRandomSeed = 3f;

	public void Init()
	{
		float currentVirginityProbability = 0.5f;

		Vector3 pos = this.transform.localPosition;
		pos.x = Random.Range(Globals.VILLAGERS_MIN_X, Globals.VILLAGERS_MAX_X);
		while(pos.x < Globals.ALTAR_MAX_X && pos.x > Globals.ALTAR_MIN_X)
			pos.x = Random.Range(Globals.VILLAGERS_MIN_X, Globals.VILLAGERS_MAX_X);
		pos.y = 0f;
		this.transform.localPosition = pos;

        ChooseVillagerMovementDirection();
		maximumTimeToChangeVillagerMovement = Random.Range(3f, 6f);
		
		Woman = Random.Range(0, 2) > 0;
		if (Woman)
		{
			int random = Random.Range(0, FemaleThoughtSprites.Count);
			ThoughtIcon.sprite = FemaleThoughtSprites[random].Icon;
			currentVirginityProbability += FemaleThoughtSprites[random].VirginityLevel;
		}
		else
		{
			int random = Random.Range(0, MaleThoughtSprites.Count);
			ThoughtIcon.sprite = MaleThoughtSprites[random].Icon;
			currentVirginityProbability += MaleThoughtSprites[random].VirginityLevel;
		}
		
		if (Woman)
		{
			int random = Random.Range(0, FemaleHatSprites.Count);
			Hat.sprite = FemaleHatSprites[random].HatSprite;
			currentVirginityProbability += FemaleHatSprites[random].VirginityLevel;
		}
		else
		{
			int random = Random.Range(0, MaleHatSprites.Count);
			Hat.sprite = MaleHatSprites[random].HatSprite;
			currentVirginityProbability += MaleHatSprites[random].VirginityLevel;
		}
		
		if (Woman)
		{
			int random = Random.Range(0, FemaleHeadSprites.Count);
			Head.sprite = FemaleHeadSprites[random].HeadSprite;
			currentVirginityProbability += FemaleHeadSprites[random].VirginityLevel;
		}
		else
		{
			int random = Random.Range(0, MaleHeadSprites.Count);
			Head.sprite = MaleHeadSprites[random].HeadSprite;
			currentVirginityProbability += MaleHeadSprites[random].VirginityLevel;
		}
		
		if (Woman)
		{
			int random = Random.Range(0, FemaleShirtSprites.Count);
			Torso.sprite = FemaleShirtSprites[random].Torso;
			LeftArm.sprite = FemaleShirtSprites[random].Arm;
			RightArm.sprite = FemaleShirtSprites[random].Arm;
			currentVirginityProbability += FemaleShirtSprites[random].VirginityLevel;
		}
		else
		{
			int random = Random.Range(0, MaleShirtSprites.Count);
			Torso.sprite = MaleShirtSprites[random].Torso;
			LeftArm.sprite = MaleShirtSprites[random].Arm;
			RightArm.sprite = MaleShirtSprites[random].Arm;
			currentVirginityProbability += MaleShirtSprites[random].VirginityLevel;
		}

		if (Woman)
		{
			int random = Random.Range(0, FemalePantsSprites.Count);
			LeftLeg.sprite = FemalePantsSprites[random].LeftLeg;
			RightLeg.sprite = FemalePantsSprites[random].RightLeg;
			currentVirginityProbability += FemalePantsSprites[random].VirginityLevel;
		}
		else
		{
			int random = Random.Range(0, MalePantsSprites.Count);
			LeftLeg.sprite = MalePantsSprites[random].LeftLeg;
			RightLeg.sprite = MalePantsSprites[random].RightLeg;
			currentVirginityProbability += MalePantsSprites[random].VirginityLevel;
		}
		
		Virginity = currentVirginityProbability;
		IsVirgin = Random.Range(0f, 1f) < currentVirginityProbability;

		HideThought();
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
			case EVillagerState.Praying:
				Globals.POWER = Mathf.Min(Globals.POWER + (Time.deltaTime * Globals.CULTIST_POWER_MOD), Globals.MAX_POWER);
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
		DOTween.To(() => this.transform.localPosition, (pos) => this.transform.localPosition = pos, Vector3.zero, 1.2f);

		//this.transform.localPosition = Vector3.zero;
		villagerAnimations.AnimationCarried();		
	}

	public void HandleBeingDropped()
	{
		CurrentState = EVillagerState.Idle;
		this.transform.SetParent(GameController.Instance.PlayerCharacter.transform.parent);
		this.transform.localPosition = GameController.Instance.PlayerCharacter.transform.localPosition;
		villagerAnimations.AnimationIdle();
	}

	public void HandleBeingDroppedAsSacrifice()
	{

		CurrentState = EVillagerState.Dying;
		this.transform.SetParent(GameController.Instance.Altar.SacrificeSpot);
		//this.transform.localPosition = GameController.Instance.Altar.SacrificeSpot.position;
		this.transform.localPosition = new Vector3(0f, -0.35f, 0f);
		villagerAnimations.AnimationDie();

		GameController.Instance.PlayerCharacter.RemoveVillagerFromColliding(this);
	}

	public void HandleBeingDroppedAsCultist(PrayerSpot prayerSpot)
	{
		CurrentState = EVillagerState.Praying;
		this.transform.SetParent(prayerSpot.transform);
		//this.transform.localPosition = prayerSpot.CultistSpot.localPosition;
		this.transform.localPosition = new Vector3(0f, -0.5f, 0f);

		if(GameController.Instance.Altar.transform.position.x > this.transform.position.x)
		{
			Vector3 newScale = this.transform.localScale;
			newScale.x = 1f;
			this.transform.localScale = newScale;
		}
		else
		{
			Vector3 newScale = this.transform.localScale;
			newScale.x = -1f;
			this.transform.localScale = newScale;
		}

		villagerAnimations.AnimationPray();

		if(GameController.Instance.PlayerCharacter != null)
			GameController.Instance.PlayerCharacter.RemoveVillagerFromColliding(this);
	}

    public void HandleBeingKilled()
    {
		CurrentState = EVillagerState.Dying;
		villagerAnimations.AnimationDie();

		GameController.Instance.PlayerCharacter.RemoveVillagerFromColliding(this);
    }

	public void HandleDieAnimationFinished()
	{
		Destroy(this.gameObject);
	}

    public void SetNewXPosition(float newX)
    {
        Vector3 pos = this.transform.localPosition;
		pos.x = Mathf.Min(Mathf.Max(Globals.MAP_MIN_X, newX), Globals.MAP_MAX_X);
		if(newX > Globals.MAP_MAX_X || newX < Globals.MAP_MIN_X || (newX > Globals.ALTAR_MIN_X && newX < Globals.ALTAR_MAX_X))
			timeToChangeVillagerMovement = maximumTimeToChangeVillagerMovement + 1f;
        this.transform.localPosition = pos;
    }

	public void ShowThought()
	{
		ThoughtBubble.gameObject.SetActive(true);
	}

	public void HideThought()
	{
		ThoughtBubble.gameObject.SetActive(false);
	}

    void OnDestroy()
    {
        if (GameController.Instance.Villagers.Contains(this))
        {
            GameController.Instance.Villagers.Remove(this);
        }
    }
}
