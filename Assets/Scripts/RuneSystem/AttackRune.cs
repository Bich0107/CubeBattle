using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRune : BaseRune
{
    [SerializeField] float attack;
    [SerializeField] GameObject projectile;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Activate();
        }
    }

    public override void Activate()
    {
        GameObject g = Instantiate(projectile, transform.position, transform.localRotation);
        BaseProjectile projectileScript = g.GetComponent<BaseProjectile>();
        projectileScript.Set(attack);
        projectileScript.Attack();
    }
}
