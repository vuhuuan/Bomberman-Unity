using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isDead = false;
    public bool IsDead { get { return isDead; } set { isDead = value; } }

    private bool _deadFunctionCalled = false;
    private bool _isInvincible = false;
    
    public Sprite playerSprite;


    private void Start()
    {
        //isDead = true;
    }

    private void Update()
    {
        if (IsDead && !_deadFunctionCalled && !_isInvincible)
        {
            PlayerDead();
            _deadFunctionCalled = true;
        }
    }

    public void PlayerDead()
    {
        if (_isInvincible) return;
        GameObject.Find("Round Manager").GetComponent<RoundManager>().numberOfAlivePlayer -= 1;
        Debug.Log(gameObject.name + " is dead!");
        StartCoroutine(PlayerDeadAnimation());

        AudioManager.Instance.Play("Bomberman Dies");
    }
    IEnumerator PlayerDeadAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }
    public void PlayerStartOrWinAnimate()
    {
        GetComponent<PlayerMovement>().StartOrWinAnimate();
    }
    public void LockMovement()
    {
        GetComponent<PlayerMovement>().LockMovement = true;
    }

    public void UnLockMovement()
    {
        GetComponent<PlayerMovement>().LockMovement = false;
    }

    public void UnLockInvincible()
    {
        _isInvincible = true;
    }
}
