using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCoinBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _specialCoinSound;
    [Range(0,2)][SerializeField] private int _specialCoinIndex;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            collision.GetComponent<UIPlayerBehaviour>().ActiveSpecialCoinAtIndex(_specialCoinIndex);
            _audio.PlayOneShot(_specialCoinSound);
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;

            Invoke("DestroyObject", 1.0f);
        }
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
