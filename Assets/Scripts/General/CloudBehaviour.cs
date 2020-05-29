using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour
{
    public float Speed { get; set; }
    public float Buffer { get; set; }
    public float CamWidth { get; set; }

    void Update()
    {
        transform.Translate(Vector2.left * Speed * Time.deltaTime);

        if (transform.position.x + Buffer < -CamWidth + Camera.main.transform.position.x)
        {
            this.gameObject.SetActive(false);
        }
    }
}
