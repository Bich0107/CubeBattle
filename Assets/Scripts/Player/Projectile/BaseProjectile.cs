using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [Header("Base projectile settings")]
    [SerializeField] protected float attack;
    [SerializeField] protected MovingObject movingObject;

    public virtual void Set(float _attack)
    {
        attack = _attack;
    }
    public abstract void Attack();
}
