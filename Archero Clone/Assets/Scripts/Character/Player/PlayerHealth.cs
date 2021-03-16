using UnityEngine;
using System.Collections;

public class PlayerHealth : LivingEntity
{
    public override bool ApplyDamage(DamageMessage damageMessage)
    {
        if (!base.ApplyDamage(damageMessage))
            return false;

        return true;
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
    }

    public override void Die()
    {
        base.Die();
    }
}
