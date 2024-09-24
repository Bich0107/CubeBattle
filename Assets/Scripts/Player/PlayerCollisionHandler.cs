using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour, IHitByObstacle
{
    void OnTriggerEnter(Collider other)
    {
        ITriggerByPlayer target = other.GetComponent<ITriggerByPlayer>();
        if (target != null)
        {
            target.Triggered();
            return;
        }
    }

    public void Hit()
    {
        Debug.Log("hit by obstacle");
    }
}
