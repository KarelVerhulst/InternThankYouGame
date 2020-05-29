using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewSide
{
    Neutral,
    Left,
    Right
}

public class CheckSide : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;

    public ViewSide LookToThe { get; private set; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            Vector2 point = this.transform.InverseTransformPoint(collision.transform.position);

            if (point.x < 0.0)
            {
                LookToThe = ViewSide.Left;
            }
            else if (point.x > 0.0)
            {
                LookToThe = ViewSide.Right;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_playerMask == (_playerMask | (1 << collision.gameObject.layer)))
        {
            LookToThe = ViewSide.Neutral;
        }
    }
}
