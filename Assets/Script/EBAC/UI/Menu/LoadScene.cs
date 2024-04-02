using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}