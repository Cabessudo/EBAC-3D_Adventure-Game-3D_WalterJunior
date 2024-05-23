using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Texts;

public class LoadScene : MonoBehaviour
{
    public int phaseLvl = 1;

    public void LoadLevel()
    {
        if(SaveManager.Instance?.setup.levelUnlocked >= phaseLvl)
        {
            SceneManager.LoadScene(phaseLvl);
            Debug.Log("Loading...");
        }
        else
            TextManagerUI.Instance?.SetTextByType(TextType.PLANET_LOCKED);
    }

    public void LastPlanetNotFinished()
    {
        if(SaveManager.Instance.setup.levelUnlocked >= phaseLvl)
            TextManagerUI.Instance?.SetTextByType(TextType.LAST_PLANET);
        else
            TextManagerUI.Instance?.SetTextByType(TextType.PLANET_LOCKED);
    }
}