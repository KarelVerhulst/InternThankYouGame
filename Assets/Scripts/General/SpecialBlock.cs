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
            BounceBlock();
            _useOneTime = false;
        }
    }

    private void BounceBlock()
    {
        Camera.main.transform.DOShakePosition(.2f, new Vector3(0, 2, 0), 0);
        _animator.SetTrigger("ActivateSpecialBox");
        _reward = Instantiate(_rewardObject, _rewardPosition.position, Quaternion.identity);

        this.gameObject.layer = LayerMask.NameToLayer("Ground");

        Invoke("DestroyObject", 0.5f);
    }

    private void DestroyObject()
    {
        _reward.SetActive(false);
    }

    public void ChangeColorBlock()
    {
        GetComponent<SpriteRenderer>().color = Color.magenta;
    }

}
