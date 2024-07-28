using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPowerUp : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioManager.Instance.Play("Item Get");

            PowerUpEffect(collision.gameObject);
            Destroy(gameObject);
        }
    }

    protected virtual void PowerUpEffect(GameObject Player)
    {
    }
}
