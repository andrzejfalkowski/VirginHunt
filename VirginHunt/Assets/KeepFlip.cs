using UnityEngine;
using System.Collections;

public class KeepFlip : MonoBehaviour 
{
	void Update () 
	{
		this.transform.localScale = this.transform.parent.localScale;
	}
}
