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
        if(SaveManager.Instance.setup.levelUnlocked >= phaseLvl)
        {
            SceneManager.LoadScene(phaseLvl);
            Debug.Log("Loading...");
        }
        else
        Debug.Log("Level Locked");
    }

    public void LastPlanetNotFinished()
    {
        if(SaveManager.Instance.setup.levelUnlocked >= phaseLvl)
            TextManagerUI.Instance.SetTextByType(TextType.LAST_PLANET);
        else
            Debug.Log("Level Locked");
    }
}