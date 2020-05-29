using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PlayerState
{
    Idle,
    Walk,
    Jump,
    Dead,
    Fall,
    Hit
}

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask topLayer;
    public float speed = 10;
    public float jumpForce = 5;
    public bool onGround;
    public Vector2 bottomOffset;
    public float collisionRadius = 0.25f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private CircleCollider2D _playerCollider;
    [SerializeField] private CircleCollider2D _bottomCollider;

    [SerializeField] private Vector2 sideOffset;
    [SerializeField] private float collisionSideRadius = 0.25f;

    [SerializeField] private List<Animator> _spriteCharacters = new List<Animator>();


    private Animator _animator = null;

    public bool hitOnTheSide;

    private bool _isFacingRight = true;
    private float _moveInput;

    private Rigidbody2D _rb;
    private PlayerState _currentState;

    private bool _oneTime = true;
    private int _lifeIndex = 3;

    public int LifeIndex
    {
        get { return _lifeIndex; }
        set
        {
            _lifeIndex = value;
        }
    }

    void Awake()
    {
        //remove this
        PlayerPrefs.SetInt("CharacterSpriteIndex", 0);
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentState = PlayerState.Idle;

        Animator characterSprite = Instantiate(_spriteCharacters[PlayerPrefs.GetInt("CharacterSpriteIndex")],this.transform);
        _animator = characterSprite;
    }

    void Update()
    {

        if (LifeIndex <= 0)
        {
            _currentState = PlayerState.Dead;
        }

        _moveInput = Input.GetAxis("Horizontal");

        if (onGround)
        {
            if (Mathf.Abs(_moveInput) > 0)
            {
                _currentState = PlayerState.Walk;
            }
            else
            {
                _currentState = PlayerState.Idle;
            }


            _animator.SetBool("IsFalling", false);
        }
        else
        {
            _currentState = PlayerState.Fall;
        }
        

        if (Input.GetButtonDown("Jump") && onGround)
        {
            _currentState = PlayerState.Jump;
        }

        if (_currentState == PlayerState.Idle)
        {

            _rb.velocity = new Vector2(0, _rb.velocity.y);
            
        }

        if (_currentState == PlayerState.Jump)
        {

            _animator.SetBool("IsFalling", true);
            _rb.velocity = Vector2.up * jumpForce;

            _currentState = PlayerState.Fall;
        }

        if (_currentState == PlayerState.Fall)
        {
            _animator.SetBool("IsFalling", true);
            if (_rb.velocity.y < 0)
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
        


    }
    void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        if (_currentState == PlayerState.Walk || _currentState == PlayerState.Fall)
        {
            
            _rb.velocity = new Vector2(_moveInput * speed, _rb.velocity.y);

            _animator.SetInteger("Movement", (int)Mathf.Abs(_rb.velocity.x));

            if (_isFacingRight && _moveInput < 0)
            {
                Flip();
            }
            else if (!_isFacingRight && _moveInput > 0)
            {
                Flip();
            }
        }

        hitOnTheSide = Physics2D.OverlapCircle((Vector2)transform.position + sideOffset, collisionSideRadius, _enemyMask);

        if (hitOnTheSide)
        {
            _animator.SetTrigger("TriggerHit");
            
            _playerCollider.enabled = false;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;

            _bottomCollider.enabled = false;

            if (_oneTime)
            {
                LifeIndex--;
                this.GetComponent<UIPlayerBehaviour>().EmptyALifeHeart(LifeIndex);
                _oneTime = false;
            }
        }
        else
        {
            _animator.ResetTrigger("TriggerHit");
            _animator.SetInteger("Movement", (int)Mathf.Abs(_rb.velocity.x));

            _playerCollider.enabled = true;
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            _bottomCollider.enabled = true;
            _oneTime = true;
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position  + sideOffset, collisionSideRadius);
    }

    private void Flip(){
        _isFacingRight = !_isFacingRight;

        Vector3 scaler = transform.localScale;
        scaler.x *= -1;

        this.transform.localScale = scaler;
    }
}
