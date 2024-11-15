using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : BaseProjectile
{
    public override void Attack()
    {
        movingObject.Move(transform.up);
    }

    void OnTriggerEnter(Collider other)
    {
        IHitByProjectile hit = other.GetComponent<IHitByProjectile>();
        if (hit != null)
        {
            hit.HitByProjectile(attack);
        }
    }
}
