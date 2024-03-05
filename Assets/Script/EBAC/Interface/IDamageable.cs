using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void Damage(int damage);
    public void Damage(int damage, Vector3 dir, float force);
}