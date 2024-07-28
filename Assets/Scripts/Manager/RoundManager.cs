using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    public int numberOfAlivePlayer;
    bool _isEndRound = false;
    bool _isEndGame = false;

    [SerializeField] private PlayerSpawner playerSpawner;

    [SerializeField] private GameOverUI gameOverUI;

    private void Awake()
    {
        _isEndRound = false;
    }

    private void Start()
    {
        HandleStartRound();
    }


    bool CheckEndRound()
    {
        return numberOfAlivePlayer <= 1 && !_isEndRound;
    }
    public void HandleEndRound()
    {
        StartCoroutine(CheckWinner());
    }
    IEnumerator CheckWinner()
    {
        yield return new WaitForSeconds(0.6f);

        Player winnerRoundPlayer = null;
        // delay time scale to determine the accurate winner
        // BIG NOTE: maybe players die at the same time so there will be no winner

        foreach (Player player in playerSpawner.PlayerInstanceList)
        {
            if (!player.IsDead)
            {
                winnerRoundPlayer = player;
            }
        }

        //winnerRoundPlayer.PlayCelebrateAnimation();

        if (winnerRoundPlayer != null)
        {
            winnerRoundPlayer.UnLockInvincible();

            foreach (PlayerData playerData in GameManager.Instance.PlayerDatas)
            {
                if (playerData.PlayerInstance == winnerRoundPlayer)
                {
                    playerData.Score++;
                    if (playerData.Score >= GameManager.Instance.MatchPoint)
                    {
                        _isEndGame = true;
                    }
                }
            }
            winnerRoundPlayer.PlayerStartOrWinAnimate();
            GameObject.Find("Player Inform UI").GetComponent<PlayerUIController>().UpdateAllPlayerScore();
        }

        // prepare to next map (coroutine maybe)
        if (_isEndGame)
        {
            AudioManager.Instance.Play("Stage Clear");
            AudioManager.Instance.Play("End Game");

            HandleEndGame(winnerRoundPlayer);
        } 
        else
        {
            yield return new WaitForSeconds(1f);

            // clear instance list
            playerSpawner.PlayerInstanceList.Clear();

            int mapIndex = Random.Range(1, GameManager.Instance.NumberOfMaps + 1);
            GameManager.Instance.SetUpMap(mapIndex);
        }
    }

    public void HandleEndGame(Player winner)
    {
        _isEndGame = true;

        GameManager.Instance.ClearPlayerData();
        gameOverUI.WinnerImage.sprite = winner.playerSprite;
        gameOverUI.gameObject.SetActive(true);
    }

    public void HandleStartRound()
    {
        _isEndRound = false;

        numberOfAlivePlayer = GameManager.Instance.NumberOfPlayer;

        playerSpawner.SpawnPlayer(GameManager.Instance.NumberOfPlayer);

        // game start animation (game start do it self)

        AudioManager.Instance.Play("Stage Intro");
    }

    private void Update()
    {
        if (CheckEndRound())
        {
            HandleEndRound();
            _isEndRound = true;
        }
    }
}
