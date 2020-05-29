using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _goToSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            PlayerPrefs.SetInt("CoinScore", collision.GetComponent<UIPlayerBehaviour>().Score);
            
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.UpArrow))
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
