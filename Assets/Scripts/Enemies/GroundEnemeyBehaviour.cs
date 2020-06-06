using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EnemyState
{
    Walking,
    Falling,
    Dead,
    Idle
}

public class GroundEnemeyBehaviour : MonoBehaviour
{
    [Header("Locomotion")]
    [SerializeField] private float _gravity;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private bool _isWalkingLeft;
    [SerializeField] private float _speed = 5f;
    [Header("LayerMasks")]
    [SerializeField] private LayerMask _playerBottomMask;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;
    [Header("Collisions settings")]
    [SerializeField] private Vector2 _bottomOffset;
    [SerializeField] private Vector2 _rightOffset;
    [SerializeField] private Vector2 _leftOffset;
    [SerializeField] private float _collisionBottomRadius = 0.25f;
    [SerializeField] private float _collisionWallRadius = 0.25f;
    [Header("Animations")]
    [SerializeField] private Animator _animator;

    private EnemyState _currentState = EnemyState.Falling;

    private Vector2 _pos;
    private Vector2 _scale;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _pos = transform.localPosition;
        _scale = transform.localScale;
        _rb = this.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (_currentState != EnemyState.Dead)
        {
            UpdateEnemyPosition();
            CheckGround();
        }
        else
        {
            Dead();
        }
            
    }

    private void UpdateEnemyPosition()
    {
        if (_currentState == EnemyState.Falling)
        {
            Fall();
        }
               
        if (_currentState == EnemyState.Walking)
        {
            Walk();
        }
    }


    private void CheckGround()
    {
        bool groundLeft = Physics2D.OverlapCircle((Vector2)transform.position + _leftOffset, _collisionWallRadius, _wallLayer);
        bool groundRight = Physics2D.OverlapCircle((Vector2)transform.position + _rightOffset, _collisionWallRadius, _wallLayer);
        bool groundBottom = Physics2D.OverlapCircle((Vector2)transform.position + _bottomOffset, _collisionBottomRadius, _groundLayer);

        if (groundBottom)
        {
            _currentState = EnemyState.Walking;

            if (groundLeft)
            {
                _isWalkingLeft = false;
            }
            else if (groundRight)
            {
                _isWalkingLeft = true;
            }
        }
    }

    private void Fall()
    {
        //Debug.Log("fall"); ;
        
    }

    private void Walk()
    {
        _pos = transform.localPosition;
        
        if (_isWalkingLeft)
        {
            _pos.x -= _velocity.x * Time.deltaTime;
            _scale.x = -1;
        }
        else
        {
            _pos.x += _velocity.x * Time.deltaTime;
            _scale.x = 1;
        }

        transform.localPosition = _pos;
        transform.localScale = _scale;
    }

    private void Dead()
    {
        _animator.SetTrigger("IsHit");

        _velocity = Vector2.zero;
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        Invoke("DestroyObject", .3f);
       
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position + _bottomOffset, _collisionBottomRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + _leftOffset, _collisionWallRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + _rightOffset, _collisionWallRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerBottomMask == (_playerBottomMask | (1 << collision.gameObject.layer)))
        {
            Camera.main.GetComponent<CameraBehaviour>().ShakeCamera();

            _currentState = EnemyState.Dead;

            collision.GetComponentInParent<Rigidbody2D>().velocity = Vector2.up * 5;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (_groundLayer == (_groundLayer | (1 << collision.gameObject.layer)))
        {
            _currentState = EnemyState.Falling;
        }
    }
}
