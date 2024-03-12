using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Ebac.Singleton;

public class EffectsManagers : Singleton<EffectsManagers>
{
    public PostProcessVolume postProcess;
    [SerializeField] private Vignette _vignette;
    public float flashDuration = 1;
    public float flashIntesity;

    [NaughtyAttributes.Button]
    public void ChangeVignete()
    {
        StartCoroutine(FlashColorVignete());
    } 

    IEnumerator FlashColorVignete()
    {
        Vignette tpm;

        if(postProcess.profile.TryGetSettings<Vignette>(out tpm))
        {
            _vignette = tpm;
        }

        float time = 0;

        while(time < flashDuration)
        {
            time += Time.deltaTime;
            _vignette.intensity.value = flashIntesity;

            ColorParameter c = new ColorParameter();
            c.value = Color.Lerp(Color.black, Color.red, time / flashDuration);

            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        time = 0;

        while(time < flashDuration)
        {
            time += Time.deltaTime;
            _vignette.intensity.value = flashIntesity - time;

            ColorParameter c = new ColorParameter();
            c.value = Color.Lerp(Color.red, Color.black, time / flashDuration);

            _vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        _vignette.intensity.value = 0;
        yield break;
    }
}
