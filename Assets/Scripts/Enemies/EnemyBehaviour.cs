using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private CheckSide _lookSide;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Vector2 _shootLeftDirection;
    [SerializeField] private Vector2 _shootRightDirection;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_lookSide.LookToThe != ViewSide.Neutral)
        {
            _animator.SetBool("IsUp", true);
        }
        else
        {
            ResetAnimation();
        }

    }

    private void LookToPlayer()
    {
        _animator.SetBool("IsDown", true);

        if (_lookSide.LookToThe == ViewSide.Right)
        {
            _animator.SetTrigger("LookRight");
        }

        if (_lookSide.LookToThe == ViewSide.Left)
        {
            _animator.SetTrigger("LookLeft");
        }

     
    }

    private void ShootProjectile(ViewSide side)
    {
        BulletBehaviour bullet = Instantiate(_bulletPref, _shootPoint.position, Quaternion.identity).GetComponent<BulletBehaviour>();

        bullet.ShootDiagonally = true;
        

        if (side == ViewSide.Right)
        {
            bullet.ShootPosition = _shootRightDirection;
        }
        else if (side == ViewSide.Left)
        {
            bullet.ShootPosition = _shootLeftDirection;
        }
    }

    private void GoDown()
    {
        _animator.SetBool("IsDown", false);
    }

    private void Idle()
    {
        _animator.SetBool("IsDown", true);
    }
   


    private void ResetAnimation()
    {
        _animator.SetBool("IsDown", false);
        _animator.SetBool("IsUp", false);
        _animator.ResetTrigger("LookRight");
        _animator.ResetTrigger("LookLeft");
        
    }
}
