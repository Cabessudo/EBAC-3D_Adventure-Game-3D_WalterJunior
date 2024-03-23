using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : HealthBase
{
    //UI
    public UIFillUpdater healthBar;

    public override void RestartLife()
    {
        base.RestartLife();
        UpdateUI();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateUI();
    }

    void UpdateUI()
    {
        if(healthBar != null) healthBar.UpdateValue((float)currLife / maxLife);
    }

    public void ChangeDamage(float damageMultiply, float time)
    {
        StartCoroutine(ChangeDamageRoutine(damageMultiply, time));
    }

    IEnumerator ChangeDamageRoutine(float damageMultiply, float time)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(time);
        this.damageMultiply = 1;
    }
}
