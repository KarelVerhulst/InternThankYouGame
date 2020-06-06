using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpecialBlock : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _rewardObject;
    [SerializeField] private Transform _rewardPosition;


    private GameObject _reward;
    private bool _useOneTime = true;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerMask == (_playerMask | (1 << other.gameObject.layer)) && _useOneTime)
        {
            BounceBlock(other);
            _useOneTime = false;
        }
    }

    private void BounceBlock(Collider2D other)
    {
        Camera.main.GetComponent<CameraBehaviour>().ShakeCamera();

        _reward = Instantiate(_rewardObject, _rewardPosition.position, Quaternion.identity);

        if (_reward.TryGetComponent<CoinBehaviour>(out CoinBehaviour coin))
        {
            coin.PlayCoinSound();
        }

        if (_reward.TryGetComponent<HeartBehaviour>(out HeartBehaviour heart))
        {
            
            //other.GetComponentInParent<UIPlayerBehaviour>().FillALifeHeart(other.GetComponentInParent<PlayerBehaviour>().LifeIndex);
            //other.GetComponentInParent<PlayerBehaviour>().LifeIndex++;
            other.GetComponentInParent<PlayerBehaviour>().IncreaseLife();
            //heart.PlayHeartSound();
        }
        

        this.gameObject.layer = LayerMask.NameToLayer("Ground");

        Invoke("DestroyObject", 0.5f);
    }

    private void DestroyObject()
    {
        _reward.SetActive(false);
    }

}
