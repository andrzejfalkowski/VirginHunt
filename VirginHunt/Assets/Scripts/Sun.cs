using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour 
{
	void Update() 
	{
		Vector3 rot = this.transform.localEulerAngles;
		if(GameController.Instance.CurrentGamePhase == GameController.EGamePhase.Day)
			rot.z = (GameController.Instance.CurrentPhaseTime / Globals.DAY_DURATION) * 180f + 270f;
		else if(GameController.Instance.CurrentGamePhase == GameController.EGamePhase.Night)
			rot.z = (GameController.Instance.CurrentPhaseTime / Globals.NIGHT_DURATION) * 180f + 90f;

		this.transform.localEulerAngles = rot;
	}
}
