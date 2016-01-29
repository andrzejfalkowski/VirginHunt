using UnityEngine;
using System.Collections;

public class Beast : MonoBehaviour 
{
	public enum EBeastState
	{
		Idle,
		RunningLeft,
		RunningRight,
		PickingUpCultist,
		Dying
	}
	public EBeastState CurrentState = EBeastState.Idle;
	public bool IsCarryingCultist = false;

	public void Init()
	{

	}
}
