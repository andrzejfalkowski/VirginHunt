using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	public void LoadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
