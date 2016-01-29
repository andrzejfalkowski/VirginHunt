using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameController.Instance.IsGameInPogress())
		{
			if(Input.GetKey(KeyCode.LeftArrow))
			{
				GameController.Instance.PlayerCharacter.MoveLeft();
			}
			else if(Input.GetKey(KeyCode.RightArrow))
			{
				GameController.Instance.PlayerCharacter.MoveRight();
			}
			else
			{
				GameController.Instance.PlayerCharacter.StopMoving();
			}
			if(Input.GetKey(KeyCode.Space))
			{
				GameController.Instance.PlayerCharacter.HandleSpaceAction();
			}
		}
	}
}
