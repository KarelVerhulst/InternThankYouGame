using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _respawnpoint;
    [SerializeField] private LayerMask _playerMask;

    Collider2D col;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            col = collision;

            if (col.TryGetComponent<PlayerBehaviour>(out PlayerBehaviour player))
            {
                player.ReduceLife();
            }

            Invoke("Respawn", 1f);
        }
    }

    private void Respawn()
    {
        col.transform.position = _respawnpoint.position;
    }
}
