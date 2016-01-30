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

	public Camera MainCamera;

	public enum EGamePhase
	{
		Prepare,
		Day,
		Night,
		GameOver
	}
	public EGamePhase CurrentGamePhase = EGamePhase.Prepare;
	public float CurrentPhaseTime = 0f;
	private float timeToSpawnBeast = 0f;

	public int DaysAmount = 0;

	public Transform SceneParent;

	// lists
	public List<Villager> Villagers = new List<Villager>();
	public List<Beast> Beasts = new List<Beast>();
	public Player PlayerCharacter;
    public Altar Altar;

	// prefabs
	[SerializeField]
	private GameObject villagerPrefab;
	[SerializeField]
	private GameObject beastPrefab;
	[SerializeField]
	private GameObject playerPrefab;
    [SerializeField]
    private GameObject altarPrefab;

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
				DaysAmount = 0;
				CleanUp();
                SpawnAltar();
				for(int i = 0; i < Globals.VILLAGERS_START_AMOUNT; i++)
				{
					SpawnVillager();
				}
				CurrentGamePhase = EGamePhase.Day;
				Globals.POWER = 0f;
				SpawnPlayer();
			break;
			case EGamePhase.Day:
				if(CurrentPhaseTime > Globals.DAY_DURATION)
				{
					CurrentPhaseTime = CurrentPhaseTime % Globals.DAY_DURATION;
					CurrentGamePhase = EGamePhase.Night;
					// TODO: handle switch to night
					timeToSpawnBeast = 0f;
				}
			break;
			case EGamePhase.Night:
				if(CurrentPhaseTime > Globals.NIGHT_DURATION)
				{
					CurrentPhaseTime = CurrentPhaseTime % Globals.NIGHT_DURATION;
					CurrentGamePhase = EGamePhase.Day;
					// TODO: handle switch to day
					for(int i = 0; i < Beasts.Count; i++)
					{
						Beasts[i].Die();
					}
					DaysAmount++;
				}
				else
				{
					timeToSpawnBeast += Time.deltaTime;
					if(timeToSpawnBeast > (Globals.NIGHT_DURATION - Globals.BEAST_MERCY_TIME) / (Globals.START_BEAST_AMOUNT + DaysAmount * Globals.BEAST_AMOUNT_INCREASE))
					{
						SpawnBeast();
						timeToSpawnBeast = 0f;
					}
				}
			break;
			case EGamePhase.GameOver:
				// TODO: menus and shit
			break;
		}
	}

	public void SpawnPlayer()
	{
		GameObject playerObject = GameObject.Instantiate(playerPrefab) as GameObject;
		playerObject.transform.SetParent(SceneParent);
		Player player = playerObject.GetComponent<Player>();
		PlayerCharacter = player;
		player.Init();
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

    public void SpawnAltar()
    {
        GameObject altarObject = GameObject.Instantiate(altarPrefab) as GameObject;
        altarObject.transform.SetParent(SceneParent);
        Altar altar = altarObject.GetComponent<Altar>();
        Altar = altar;
        altar.Init();
    }

    public void CleanUp()
	{
		for(int i = 0; i < Villagers.Count; i++)
		{
			Destroy(Villagers[i].gameObject);
		}
		Villagers.Clear();

		for(int i = 0; i < Beasts.Count; i++)
		{
			Destroy(Beasts[i].gameObject);
		}
		Beasts.Clear();

		if(PlayerCharacter != null)
			Destroy(PlayerCharacter.gameObject);
	}

	public void GameOver()
	{
		CurrentGamePhase = EGamePhase.GameOver;
	}
}
