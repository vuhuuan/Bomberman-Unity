using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    public PlayerUI[] playerUIs;

    private void Start()
    {
        UpdateAllPlayerScore();
    }

    public void UpdateAllPlayerScore()
    {
        switch (GameManager.Instance.NumberOfPlayer)
        {
            case 2:
                playerUIs[0].UpdateScore(GameManager.Instance.PlayerDatas[0].Score);
                playerUIs[1].UpdateScore(GameManager.Instance.PlayerDatas[1].Score);

                playerUIs[0].gameObject.SetActive(true);
                playerUIs[1].gameObject.SetActive(true);

                break;
            case 3:
                playerUIs[0].UpdateScore(GameManager.Instance.PlayerDatas[0].Score);
                playerUIs[1].UpdateScore(GameManager.Instance.PlayerDatas[1].Score);
                playerUIs[2].UpdateScore(GameManager.Instance.PlayerDatas[2].Score);

                playerUIs[0].gameObject.SetActive(true);
                playerUIs[1].gameObject.SetActive(true);
                playerUIs[2].gameObject.SetActive(true);
                break;
        }
    }
}
