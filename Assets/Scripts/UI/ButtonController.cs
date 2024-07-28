using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void PlayerClickSound()
    {
        AudioManager.Instance.Play("Item Get");
    }
    public void SelectMode(int numberOfPlayer)
    {
        GameManager.Instance.HandleStartGame(numberOfPlayer);
    }
}
