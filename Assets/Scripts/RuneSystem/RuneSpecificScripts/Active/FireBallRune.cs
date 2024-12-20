using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallRune : ActiveRune, IEffectedBySizeEffect, IEffectedByAttackPowerEffect
{
    [SerializeField] float attack;
    [SerializeField] Vector3 size;
    [SerializeField] GameObject projectile;
    Vector3 currentSize;
    float currentAttack;

    void OnEnable()
    {
        currentAttack = attack;
    }

    public override void Activate(object _obj = null)
    {
        GameObject g = Instantiate(projectile, transform.position, transform.localRotation);
        g.transform.localScale = currentSize;
        BaseProjectile projectileScript = g.GetComponent<BaseProjectile>();
        projectileScript.Set(currentAttack);
        projectileScript.Attack();

        LogSystem.Instance.Log($"Fire ball rune activated with attack: {currentAttack} & size: {currentSize}");
    }

    public void Effect_SizeChange(float _sizeIncreasePercent)
    {
        LogSystem.Instance.Log($"Fire ball's size is increased!");
        currentSize = size * (1f + _sizeIncreasePercent);
    }

    public void Effect_AttackPowerChange(float _attackIncreasePercent)
    {
        LogSystem.Instance.Log($"Fire ball's attack is increased!");
        currentAttack = attack * (1f + _attackIncreasePercent);
    }

    public void Reset()
    {
        currentSize = size;
        currentAttack = attack;
    }
}
