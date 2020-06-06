using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;

    private float _countDownTimer =20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            HitPlayer(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            _countDownTimer -= Time.deltaTime;

            if (_countDownTimer <= 0)
            {
                HitPlayer(collision);
                _countDownTimer = 20;
            }
        }
    }

    private void HitPlayer(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour player))
        {
            player.ReduceLife();
            player.PlayHitAnimation();
        }
    }
}
