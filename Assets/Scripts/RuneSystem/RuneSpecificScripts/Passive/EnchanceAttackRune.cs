using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchanceAttackRune : PassiveRune
{
    public override void Empower(GameObject _activeRune)
    {
        if (_activeRune == null) return;

        IEffectedByAttackPowerEffect target = _activeRune.GetComponent<IEffectedByAttackPowerEffect>();
        if (target != null)
        {
            target.Effect_AttackPowerChange(value);
        }
    }
}
