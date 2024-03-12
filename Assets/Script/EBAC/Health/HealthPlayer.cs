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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateUI();
    }

    void UpdateUI()
    {
        if(healthBar != null) healthBar.UpdateValue((float)currLife / maxLife);
    }
}
