using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingPlantBehaviour : MonoBehaviour
{
    [SerializeField] private CheckSide _lookSide;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Vector2 _shootLeftDirection;
    [SerializeField] private Vector2 _shootRightDirection;

    private Vector2 scale = Vector2.one;

    // Start is called before the first frame update
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

        if (_lookSide.LookToThe == ViewSide.Right)
        {
            _animator.SetTrigger("LookRight");
            scale.x = 1;
        }

        if (_lookSide.LookToThe == ViewSide.Left)
        {
            _animator.SetTrigger("LookLeft");
            scale.x = -1;
        }

        this.transform.localScale = scale;

    }

    private void ShootProjectile()
    {
        BulletBehaviour bullet = Instantiate(_bulletPref, _shootPoint.position, Quaternion.identity).GetComponent<BulletBehaviour>();

        bullet.ShootDiagonally = true;


        if (_lookSide.LookToThe == ViewSide.Right)
        {
            bullet.ShootPosition = _shootRightDirection;
        }
        else if (_lookSide.LookToThe == ViewSide.Left)
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
        _animator.SetBool("IsUp", false);
        _animator.ResetTrigger("LookRight");
        _animator.ResetTrigger("LookLeft");


    }
}
