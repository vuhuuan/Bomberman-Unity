using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static ItemList Instance;
    public List<GameObject> ItemPowerUpList;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    public GameObject GetRandomItem()
    {
        int randomIndex = Random.Range(0, ItemPowerUpList.Count);
        return ItemPowerUpList[randomIndex];
    }
}
