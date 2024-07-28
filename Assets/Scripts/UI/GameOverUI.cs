using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Image WinnerImage;
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
