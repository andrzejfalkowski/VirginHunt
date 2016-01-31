using UnityEngine;
using System.Collections;

public class WinGame : MonoBehaviour {

    public GameObject WinGamePanel;
	
	// Update is called once per frame
	void Update () {
	    if (Globals.POWER >= Globals.MAX_POWER)
        {
            WinGamePanel.SetActive(true);
        }
	}
}
