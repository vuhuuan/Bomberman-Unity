using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUpItem : ItemPowerUp
{
    protected override void PowerUpEffect(GameObject Player)
    {
        Player.GetComponent<BombController>().AddBomb();
    }
}
