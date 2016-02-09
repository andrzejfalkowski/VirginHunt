using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour 
{
	public SpriteRenderer SunBody;
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

		float power = 0.7f + 0.7f * Mathf.Abs((Globals.DAY_DURATION / 2f) - GameController.Instance.CurrentPhaseTime) / (Globals.DAY_DURATION / 2f);
		SunGlow.transform.localScale = new Vector3(power, power, 0f);
		SunBody.transform.localScale = new Vector3(power, power, 0f);
	}
}
