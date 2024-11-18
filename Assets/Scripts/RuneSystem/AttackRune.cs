using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRune : BaseRune
{
    [SerializeField] float attack;
    [SerializeField] GameObject projectile;

    public override void Activate()
    {
        GameObject g = Instantiate(projectile, transform.position, transform.localRotation);
        BaseProjectile projectileScript = g.GetComponent<BaseProjectile>();
        projectileScript.Set(attack);
        projectileScript.Attack();
    }
}
