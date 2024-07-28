using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionUpItem : ItemPowerUp
{
    protected override void PowerUpEffect(GameObject Player)
    {
        Player.GetComponent<BombController>().explosionRadius += 1;
    }
}
