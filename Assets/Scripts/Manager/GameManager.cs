using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Start Game")]
    public static GameManager Instance;
    public List<PlayerData> PlayerDatas;
    public int NumberOfPlayer = 3;
    public int NumberOfMaps = 3;
    public int CurrentMapIndex;

    [Header("End Game")]
    //public bool IsEndGame = false;
    //public Player Winner;
    public int MatchPoint = 3;


    // call after click a button (or whatever event)
    public void HandleStartGame(int numberOfPlayer)
    {
        // number of players
        NumberOfPlayer = numberOfPlayer;

        // reset players data
        SetUpPlayerData();

        // load random round map
        // handle start round is inside SetUpMap function
        int mapIndex = Random.Range(1, NumberOfMaps);
        SetUpMap(mapIndex);
    }

    void SetUpPlayerData()
    {
        // maybe should clear Player Data before create new.
        ClearPlayerData();

        for (int i = 0; i < NumberOfPlayer; i++)
        {
            PlayerData playerData = new PlayerData("Player " + (i + 1));
            PlayerDatas.Add(playerData);
        }
    }

    public void ClearPlayerData()
    {
        PlayerDatas.Clear();
    }

    public void SetUpMap(int mapIndex)
    {
        // load scene
        StartCoroutine(DelayLoadMap(mapIndex));

        // wait for scene to load player

    }
    private IEnumerator DelayLoadMap(int mapIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mapIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        CurrentMapIndex = mapIndex;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        PlayerDatas = new List<PlayerData>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

}

public class PlayerData
{
    public PlayerData (string name)
    {
        this.Name = name;
        this.Score = 0;
    }
    public Player PlayerInstance { get; set; }
    public string Name { get; set; }

    public int Score;

    public void ShowInform ()
    {
        Debug.Log(Name + ": " + Score);
    }
}