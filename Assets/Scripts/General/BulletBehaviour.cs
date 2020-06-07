using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private float _speed = 20;


    private float _countDownTimer = 5;

    public Vector2 ShootPosition { get; set; }
    public bool ShootDiagonally { get; set; }

    // Update is called once per frame
    void Update()
    {
        if (ShootDiagonally)
        {
            ShootDiagnolly();
        }
        else
        {
            ShootWithVelocity();
        }

        _countDownTimer -= Time.deltaTime;

        if (_countDownTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hitMask == (_hitMask | (1 << collision.gameObject.layer)))
        {
            if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour player))
            {
                player.PlayHitAnimation();
                player.ReduceLife();
            }
        }
    }

    private void ShootDiagnolly()
    {
        Vector2 shootpos = this.transform.position;
        shootpos.x += ShootPosition.x * Time.deltaTime * _speed;
        shootpos.y += ShootPosition.y * Time.deltaTime * _speed;

        this.transform.position = shootpos;
    }

    private void ShootWithVelocity()
    {
        this.GetComponent<Rigidbody2D>().velocity = ShootPosition * Time.deltaTime * _speed;
    }

}
