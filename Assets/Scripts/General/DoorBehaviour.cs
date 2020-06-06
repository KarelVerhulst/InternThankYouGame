using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _goToSceneIndex;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            PlayerPrefs.SetInt("CoinScore", collision.GetComponent<UIPlayerBehaviour>().Score);
            
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                _animator.SetTrigger("OpenDoor");
                Invoke("GoToScene", 2f);
            }
        }
    }

    private void GoToScene()
    {
        SceneManager.LoadScene(_goToSceneIndex);
    }
}
