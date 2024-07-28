using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundStartUI : MonoBehaviour
{
    [SerializeField] private PlayerSpawner PlayerSpawner;
   
    private void Start()
    {
        //RunStartAnimation();
    }

    //private void OnEnable()
    //{
    //    RunStartAnimation();
    //}


    public void RunStartAnimation()
    {
        transform.Find("Ready Text").GetComponent<TextMeshProUGUI>().text = "Ready";
        StartCoroutine(ReadyToGo());
    }

    IEnumerator ReadyToGo()
    {
        yield return new WaitForSeconds(1f);
        transform.Find("Ready Text").GetComponent<TextMeshProUGUI>().text = "Go";

        yield return new WaitForSeconds(0.6f);

        PlayerSpawner.UnLockAllPlayerMovement();
        gameObject.SetActive(false);
    }
}
