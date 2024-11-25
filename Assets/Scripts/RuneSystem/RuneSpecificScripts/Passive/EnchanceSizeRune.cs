using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchanceSizeRune : PassiveRune
{
    public override void Empower(GameObject _activeRune)
    {
        if (_activeRune == null) return;

        IEffectedBySizeEffect target = _activeRune.GetComponent<IEffectedBySizeEffect>();
        if (target != null)
        {
            target.Effect_SizeChange(value);
        }
    }
}
