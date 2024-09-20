using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    [SerializeField] float moveSpeed;
    [SerializeField] bool moveOnAwake = false;
    bool moving = false;

    void Awake()
    {
        if (moveOnAwake)
        {
            moving = true;
        }
    }

    public float MoveSpeed
    {
        set { moveSpeed = value; }
    }

    public void Move(Vector3 _direction)
    {
        direction = _direction;
        moving = true;
    }

    void Moving()
    {
        transform.position += direction * moveSpeed * Time.fixedDeltaTime;
    }

    void FixedUpdate()
    {
        if (moving) Moving();
    }

    public void Stop() => moving = false;
}
