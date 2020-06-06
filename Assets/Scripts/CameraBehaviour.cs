using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2 _smoothTime;
    [SerializeField] private GameObject _player;
    [SerializeField] private bool _useBounds;
    [SerializeField] private Vector3 _minCameraPos;
    [SerializeField] private Vector3 _maxCameraPos;

    [Header("Shake Options")]
    [SerializeField] private float _shakeDuration = 0;
    [SerializeField] private Vector3 _shakeStrength = Vector3.one;


    private Vector2 _velocity;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, _player.transform.position.x, ref _velocity.x, _smoothTime.x);
        float posY = Mathf.SmoothDamp(transform.position.y, _player.transform.position.y, ref _velocity.y, _smoothTime.y);

        transform.position = new Vector3(posX, posY, transform.position.z);

        if (_useBounds)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, _minCameraPos.x, _maxCameraPos.x),
                Mathf.Clamp(transform.position.y, _minCameraPos.y, _maxCameraPos.y),
                Mathf.Clamp(transform.position.z, _minCameraPos.z, _maxCameraPos.z));
        }
    }


    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(_shakeDuration, _shakeStrength);
    }
}






