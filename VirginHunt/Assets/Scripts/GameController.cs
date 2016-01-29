using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public float CurrentPhaseTime = 0f;
	
	public Transform SceneParent;

	// lists
	public List<Villager> Villagers = new List<Villager>();
	public List<Beast> Beasts = new List<Beast>();

	// prefabs
	[SerializeField]
	private GameObject villagerPrefab;
	[SerializeField]
	private GameObject beastPrefab;

	public bool IsGameInPogress()
	{
		return CurrentGamePhase == EGamePhase.Day || CurrentGamePhase == EGamePhase.Night;
	}

	// Use this for initialization
	void Start () 
	{
		CurrentGamePhase = EGamePhase.Prepare;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(IsGameInPogress())
			CurrentPhaseTime += Time.deltaTime;

		switch(CurrentGamePhase)
		{
			case EGamePhase.Prepare:
				CleanUp();
				for(int i = 0; i < Globals.VILLAGERS_START_AMOUNT; i++)
				{
					SpawnVillager();
				}
				CurrentGamePhase = EGamePhase.Day;
			break;
			case EGamePhase.Day:
				if(CurrentPhaseTime > Globals.DAY_DURATION)
				{
					CurrentPhaseTime = CurrentPhaseTime % Globals.DAY_DURATION;
					CurrentGamePhase = EGamePhase.Night;
					// TODO: handle switch to night
				}
			break;
			case EGamePhase.Night:
				if(CurrentPhaseTime > Globals.NIGHT_DURATION)
				{
					CurrentPhaseTime = CurrentPhaseTime % Globals.NIGHT_DURATION;
					CurrentGamePhase = EGamePhase.Night;
					// TODO: handle switch to day
				}
			break;
			case EGamePhase.GameOver:
				// TODO: menus and shit
			break;
		}
	}

	public void SpawnVillager()
	{
		GameObject villagerObject = GameObject.Instantiate(villagerPrefab) as GameObject;
		villagerObject.transform.SetParent(SceneParent);
		Villager villager = villagerObject.GetComponent<Villager>();
		villager.Init();
		Villagers.Add(villager);
	}

	public void SpawnBeast()
	{
		GameObject beastObject = GameObject.Instantiate(beastPrefab) as GameObject;
		beastObject.transform.SetParent(SceneParent);
		Beast beast = beastObject.GetComponent<Beast>();
		beast.Init();
		Beasts.Add(beast);
	}

	public void CleanUp()
	{
		for(int i = 0; i < Villagers.Count; i++)
		{
			Destroy(Villagers[i]);
		}
		Villagers.Clear();

		for(int i = 0; i < Beasts.Count; i++)
		{
			Destroy(Beasts[i]);
		}
		Beasts.Clear();
	}
}
