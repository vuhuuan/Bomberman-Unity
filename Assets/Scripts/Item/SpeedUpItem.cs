using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : ItemPowerUp
{
    protected override void PowerUpEffect(GameObject Player)
    {
        Player.GetComponent<PlayerMovement>().Speed += 1f;
    }
}
