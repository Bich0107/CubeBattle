using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] Transform targetTrans;
    [SerializeField] Vector3 offset;
    [SerializeField] bool isFollowing;

    void Start()
    {
        offset = transform.position - targetTrans.position;
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        if (!isFollowing) return;
        transform.position = targetTrans.position + offset;
    }
}
