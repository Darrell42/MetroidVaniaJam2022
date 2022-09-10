using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void ApplyDamage(float damage);
    public void ApplyDamage(float damage, Vector3 direction);
}
