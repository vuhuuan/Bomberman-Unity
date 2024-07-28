using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Player[] PlayerPrefabList;
    public List<Player> PlayerInstanceList;
    public List<Vector2> PlayerPosition;

    private void Awake()
    {
    }
    public void SpawnPlayer(int numberOfPlayer)
    {
        switch (numberOfPlayer)
        {
            case 2:
                SpawnPlayerOne();
                SpawnPlayerTwo();
                break;
            case 3:
                SpawnPlayerOne();
                SpawnPlayerTwo();
                SpawnPlayerThree();
                break;
            case 4:
                SpawnPlayerOne();
                SpawnPlayerTwo();
                SpawnPlayerThree();
                SpawnPlayerFour();
                break;
            default:
                SpawnPlayerOne();
                SpawnPlayerTwo();
                break;
        }

        PlayerStartAnimation();
    }


    public void SpawnPlayerOne()
    {
        Player player1 = Instantiate(PlayerPrefabList[0], PlayerPosition[0], Quaternion.identity);
        PlayerInstanceList.Add(player1);
        //Debug.Log(_playerDatas.Count);

        //link player data to the player instance
        GameManager.Instance.PlayerDatas[0].PlayerInstance = player1;

        // player start animation + game start animation.
        Debug.Log("All player show start animation now");
    }

    public void SpawnPlayerTwo()
    {
        Player player2 = Instantiate(PlayerPrefabList[1], PlayerPosition[1], Quaternion.identity);
        PlayerInstanceList.Add(player2);
        GameManager.Instance.PlayerDatas[1].PlayerInstance = player2;

    }

    public void SpawnPlayerThree()
    {
        Player player3 = Instantiate(PlayerPrefabList[2], PlayerPosition[2], Quaternion.identity);
        PlayerInstanceList.Add(player3);
        GameManager.Instance.PlayerDatas[2].PlayerInstance = player3;
    }

    public void SpawnPlayerFour()
    {

    }

    public void PlayerStartAnimation()
    {
        foreach (var player in PlayerInstanceList)
        {
            player.LockMovement(); // unlock in Game Start UI. (after the UI turn off)

            player.PlayerStartOrWinAnimate();
        }
        GameObject.Find("Game Start UI").GetComponent<RoundStartUI>().RunStartAnimation();
    }

    public void UnLockAllPlayerMovement()
    {
        Debug.Log("Unlock all movement from GameManager");
        foreach (var player in PlayerInstanceList)
        {
            player.UnLockMovement();
        }
    }

    public void LockAllPlayerMovement()
    {
        foreach (var player in PlayerInstanceList)
        {
            player.LockMovement();
        }
    }
}
