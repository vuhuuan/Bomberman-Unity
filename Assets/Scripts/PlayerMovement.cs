using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private KeyCode inputUp = KeyCode.W;
    private KeyCode inputDown = KeyCode.S;
    private KeyCode inputLeft = KeyCode.A;
    private KeyCode inputRight = KeyCode.D;

    [Range(1, 4)]
    public int Keyboard;

    Vector2 _direction = Vector2.zero;
    [SerializeField] float _speed = 5f;
    public float Speed { get { return _speed; } set { _speed = value; } }

    public AnimatedSpriteRenderer animationUp;
    public AnimatedSpriteRenderer animationDown;
    public AnimatedSpriteRenderer animationLeft;
    public AnimatedSpriteRenderer animationRight;
    public AnimatedSpriteRenderer animationDead;
    public AnimatedSpriteRenderer animationWin;

    private AnimatedSpriteRenderer _currentAnimation;

    Rigidbody2D rb;
    Player player;
    [SerializeField] private bool lockMovement;
    public bool LockMovement { get { return lockMovement; } set { lockMovement = value; } }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();

        _currentAnimation = animationDown;
        SetDirection(Vector2.zero, _currentAnimation);
        SetUpKeyBoard(Keyboard);
    }

    // Update is called once per frame
    void Update()
    {
        if (lockMovement) return;

        if (player.IsDead)
        {
            SetDirection(Vector2.zero, animationDead);
            lockMovement = true;
            return;
        }

        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, animationUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, animationDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, animationLeft);
        }
        else if(Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, animationRight);
        } else
        {
            SetDirection(Vector2.zero, _currentAnimation);
        }
    }

    private void FixedUpdate()
    {

        if (player.IsDead || lockMovement) return;

        Vector2 pos = transform.position;
        Vector2 translation = _direction * _speed * Time.fixedDeltaTime;

        rb.MovePosition(pos + translation);
    }

    void SetDirection(Vector2 direction, AnimatedSpriteRenderer animation)
    {
        _direction = direction;
        _currentAnimation = animation;

        animationUp.enabled = (animationUp == animation);
        animationDown.enabled = (animationDown == animation);
        animationLeft.enabled = (animationLeft == animation);
        animationRight.enabled = (animationRight == animation);
        animationDead.enabled = (animationDead == animation);
        animationWin.enabled = (animationWin == animation);

        if (!player.IsDead) _currentAnimation.idle = (direction == Vector2.zero);
    }
    public void StartOrWinAnimate()
    {
        lockMovement = true;

        animationUp.enabled = false;
        animationDown.enabled = false;
        animationLeft.enabled = false;
        animationRight.enabled = false;
        animationDead.enabled = false;
        animationWin.enabled = true;
    }

    public void SetUpKeyBoard(int setupIndex)
    {
        switch (setupIndex)
        {
            case 1:
                inputUp = KeyCode.W;
                inputDown = KeyCode.S;
                inputLeft = KeyCode.A;
                inputRight = KeyCode.D;
                GetComponent<BombController>().placeBombKeycode = KeyCode.LeftShift;
                break;
            case 2:
                inputUp = KeyCode.UpArrow;
                inputDown = KeyCode.DownArrow;
                inputLeft = KeyCode.LeftArrow;
                inputRight = KeyCode.RightArrow;
                GetComponent<BombController>().placeBombKeycode = KeyCode.RightShift;
                break;
            case 3:
                inputUp = KeyCode.I;
                inputDown = KeyCode.K;
                inputLeft = KeyCode.J;
                inputRight = KeyCode.L;
                GetComponent<BombController>().placeBombKeycode = KeyCode.RightShift;
                break;
        }
    }
}
