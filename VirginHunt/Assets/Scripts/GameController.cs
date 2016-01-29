using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	#region Singleton
	private static GameController _instance = null;
	
	public static GameController Instance
	{
		get { return _instance; }
	}
	
	private void Awake()
	{
		if (_instance != null)
		{
			Destroy(gameObject);
			return;
		}
		_instance = this;
		DontDestroyOnLoad(gameObject);
	}
	#endregion

	public enum EGamePhase
	{
		Prepare,
		Day,
		Night,
		GameOver
	}
	public EGamePhase CurrentGamePhase = EGamePhase.Prepare;
	private float CurrentPhaseTime = 0f;

	public bool IsGameInPogress()
	{
		return CurrentGamePhase == EGamePhase.Day || CurrentGamePhase == EGamePhase.Night;
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
