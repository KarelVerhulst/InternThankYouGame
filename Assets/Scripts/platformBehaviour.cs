using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformBehaviour : MonoBehaviour
{

    [SerializeField] private LayerMask _playerMask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            collision.collider.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
