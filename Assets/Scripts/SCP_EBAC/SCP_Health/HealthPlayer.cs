using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : HealthBase
{
    //UI
    public UIFillUpdater healthBar;

    protected override void Init()
    {
        if(SaveManager.Instance.setup.playerHealth > 0)
            currLife = SaveManager.Instance.setup.playerHealth;
        else
            RestartLife();
            
        UpdateUI();
        _rb = GetComponent<Rigidbody>();
    }

    [NaughtyAttributes.Button]
    void DamagePlayer()
    {
        TakeDamage(1);
    }

    public override void RestartLife()
    {
        base.RestartLife();
        UpdateUI();
        SaveManager.Instance.SavePlayerHealth();
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

    #region Cloth PWUP
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
    #endregion
}
