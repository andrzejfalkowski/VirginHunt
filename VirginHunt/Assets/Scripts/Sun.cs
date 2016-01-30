using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour 
{
	public SpriteRenderer SunGlow;
	void Update() 
	{
		Vector3 rot = this.transform.localEulerAngles;
		if(GameController.Instance.CurrentGamePhase == GameController.EGamePhase.Day)
		{
			rot.z = (GameController.Instance.CurrentPhaseTime / Globals.DAY_DURATION) * 180f + 270f;

			Color color = SunGlow.color;
			if(GameController.Instance.CurrentPhaseTime < Globals.DAY_DURATION * 0.5f)
				color.a = (GameController.Instance.CurrentPhaseTime * 2f / Globals.DAY_DURATION);
			else
				color.a = ((Globals.DAY_DURATION - GameController.Instance.CurrentPhaseTime) * 2f / Globals.DAY_DURATION);
			SunGlow.color = color;
		}
		else if(GameController.Instance.CurrentGamePhase == GameController.EGamePhase.Night)
			rot.z = (GameController.Instance.CurrentPhaseTime / Globals.NIGHT_DURATION) * 180f + 90f;

		this.transform.localEulerAngles = rot;
	}
}
