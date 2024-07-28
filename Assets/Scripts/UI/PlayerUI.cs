using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI playerScoreUI;

    private void Start()
    {
    }
    public void UpdateScore (int score)
    {
        playerScoreUI.text = score.ToString(); 
    }
}
