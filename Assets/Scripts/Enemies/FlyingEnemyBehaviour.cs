using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] Waypoints;
    [SerializeField] private float _speed = 1;
    [SerializeField] Transform _shootPosition;
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private Vector2 _shootDirection;
    [SerializeField] private Transform _flyingSprite;

    private int _index = 0;
    private float _countDownTimer = 4;
    private Vector2 _scale = Vector2.one;

    // Start is called before the first frame update
    void Start()
    {
        _scale = _flyingSprite.localScale;
        this.transform.position = Waypoints[_index].position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemyBetweenWaypoints();

        _countDownTimer -= Time.deltaTime;

        if (_countDownTimer <= 0)
        {
            BulletBehaviour bullet = Instantiate(_bulletPref, _shootPosition.position, Quaternion.identity).GetComponent<BulletBehaviour>();

            bullet.ShootDiagonally = true;
            bullet.ShootPosition = _shootDirection;

            _countDownTimer = 5;
        }
    }

    private void MoveEnemyBetweenWaypoints()
    {
        if (Vector3.Distance(transform.position, Waypoints[_index].position) <= _speed * Time.deltaTime)
        {
            ++_index;
            _index %= Waypoints.Length;

            _shootDirection.x *= -1;

            _scale.x *= -1;

            _flyingSprite.localScale = _scale;

        }

        this.transform.position = Vector3.MoveTowards(transform.position, Waypoints[_index].transform.position, _speed * Time.deltaTime);
    }
}
