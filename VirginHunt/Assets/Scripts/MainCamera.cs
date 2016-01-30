using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour 
{
	public void SetNewXPosition(float newX)
	{
		Vector3 pos = this.transform.localPosition;
		pos.x = newX;
		this.transform.localPosition = pos;
	}
}
