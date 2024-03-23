using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Damage(float damage);
    public void Damage(float damage, Vector3 dir, float force);
}
