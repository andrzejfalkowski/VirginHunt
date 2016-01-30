using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	public void LoadLevel()
    {
        Application.LoadLevel("GameScene");
    }
}
