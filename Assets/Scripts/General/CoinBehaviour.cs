using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip _coinSound;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            collision.GetComponent<UIPlayerBehaviour>().Score++;
            PlayCoinSound();
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = false;

            Invoke("DestroyObject", 1.0f);
        }
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    public void PlayCoinSound()
    {
        _audio.PlayOneShot(_coinSound);
    }
}
