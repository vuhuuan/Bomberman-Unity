using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BombController : MonoBehaviour
{
    private PlayerMovement player;
    [Header("Bomb")]
    public KeyCode placeBombKeycode = KeyCode.Space;
    public GameObject bombPrefab;
    public float bombFuseTime = 3f;
    public int bombAmount = 1;
    public int bombRemaining = 1;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;
    public LayerMask explosionLayermask;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible brickExplosionPrefab;
    public Destructible otherExplosionPrefab;


    private void Awake()
    {
        destructibleTiles = GameObject.Find("Destructibles").GetComponent<Tilemap>();
        bombRemaining = bombAmount;
        player = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (player.LockMovement) return;
        if (bombRemaining > 0 && Input.GetKeyDown(placeBombKeycode))
        {
            PlaceBomb();
        }
    }

    void PlaceBomb ()
    {
        if (player.LockMovement) return;

        Vector3 bombPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

        GameObject bomb = Instantiate(bombPrefab, bombPos, Quaternion.identity);
        bombRemaining --;
        StartCoroutine(WaitForExplode(bomb));

        // player cannot walk through a bomb after go outside it

        // player cannot place a bomb at a same tile if it already have one

        AudioManager.Instance.Play("Place Bomb");
    }

    IEnumerator WaitForExplode(GameObject bomb)
    {
        yield return new WaitForSeconds(bombFuseTime);
        // explode function
        BomExplode(bomb);

        AudioManager.Instance.Play("Bomb Explodes");
    }

    void BomExplode(GameObject bomb)
    {
        // ..
        // after explode that remain bomb plus
        bombRemaining++;
        Vector3 explodePos = new Vector3(Mathf.Round(bomb.transform.position.x), Mathf.Round(bomb.transform.position.y));
        Destroy(bomb);

        Explosion explosion = Instantiate(explosionPrefab, explodePos, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);
            
        ExpandExplode(explodePos, Vector2.up, explosionRadius);
        ExpandExplode(explodePos, Vector2.down, explosionRadius);
        ExpandExplode(explodePos, Vector2.left, explosionRadius);
        ExpandExplode(explodePos, Vector2.right, explosionRadius);

    }

    void ExpandExplode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2, 0f, explosionLayermask))
        {
            ClearDestructible(position);
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);


        ExpandExplode(position, direction, length - 1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            collision.GetComponent<Collider2D>().isTrigger = false;
        }
    }

    public void ClearDestructible (Vector2 position)
    {
        Vector3Int cellPos = destructibleTiles.WorldToCell(position);

        TileBase tile = destructibleTiles.GetTile(cellPos);

        if (tile != null) 
        {
            switch (tile.name)
            {
                case "Brick":
                    Instantiate(brickExplosionPrefab, position, Quaternion.identity);
                    break;
                case "Other Brick":
                    Instantiate(otherExplosionPrefab, position, Quaternion.identity);
                    break;

            }
            destructibleTiles.SetTile(cellPos, null);
        }
    }

    public void AddBomb()
    {
        bombAmount++;
        bombRemaining++;
    }
}
