using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour 
{
	private float xModifier = 0f;
	private float yModifier = 0f;

	private float xStartShift = 0f;
	private float yStartShift = 0f;

	void Start() 
	{
		xStartShift = this.transform.localPosition.x;
		yStartShift = this.transform.localPosition.y;
	}

	void Update() 
	{
		Vector3 pos = this.transform.localPosition;
		pos.x = xStartShift + GameController.Instance.MainCamera.transform.localPosition.x * xModifier;
		pos.y = yStartShift + GameController.Instance.MainCamera.transform.localPosition.y * yModifier;
	}
}
