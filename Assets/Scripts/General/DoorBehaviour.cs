using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _goToSceneIndex;
    [SerializeField] private GameObject _arrow;

    void Start()
    {
        _arrow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            _arrow.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                PlayerPrefs.SetInt("CoinScore", collision.GetComponent<UIPlayerBehaviour>().Score);
                _arrow.SetActive(false);

                _animator.SetTrigger("OpenDoor");
                Invoke("GoToScene", 2f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            _arrow.SetActive(false);
        }
    }

    private void GoToScene()
    {
        SceneManager.LoadScene(_goToSceneIndex);
    }
}
