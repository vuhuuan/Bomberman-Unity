using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;

    [Range(0f, 1f)]
    public float DropRate = 0.2f;

    private void Start()
    {
        // spawn some item
        DropItem();
        Destroy(gameObject, destructionTime);
    }

    void DropItem()
    {
        float randomRate = Random.Range(0f, 1f);

        if (randomRate <= DropRate)
        {
            Instantiate(ItemList.Instance.GetRandomItem(), transform.position, Quaternion.identity);
        }
    }

}
