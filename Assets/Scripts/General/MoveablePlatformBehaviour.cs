using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatformBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] Waypoints;
    [SerializeField] private float _speed = 1;
    [SerializeField] private LayerMask _playerMask;


    private int _index = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Waypoints[_index].position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatformBetweenWaypoints();
    }

    private void MovePlatformBetweenWaypoints()
    {
        if (Vector3.Distance(transform.position, Waypoints[_index].position) <= _speed * Time.deltaTime)
        {
            ++_index;
            _index %= Waypoints.Length;
        }

        this.transform.position = Vector3.MoveTowards(transform.position, Waypoints[_index].transform.position, _speed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            collision.collider.transform.SetParent(this.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            collision.collider.transform.SetParent(null);
        }
    }

}
