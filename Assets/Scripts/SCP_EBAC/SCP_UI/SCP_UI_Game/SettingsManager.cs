using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsButtons;
    public GameObject controls;
    public GameObject audioButtons;
    public GameObject mainBackground;
    public List<GameObject> backgroundSettings;
    private bool showedSettings;
    public bool menu = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }    

        
        if(Input.GetKeyDown(KeyCode.Escape) && showedSettings)
        {
            BackToPause();
        }
    }

    public void Pause()
    {
        if(!showedSettings)
        {
            settingsButtons.SetActive(true);
            mainBackground.SetActive(true);
            showedSettings = true;
            FreeCursor();
        }
        else
        {
            settingsButtons.SetActive(false);
            mainBackground.SetActive(false);
            showedSettings = false;
            backgroundSettings.ForEach(i => i.SetActive(false));
            audioButtons.SetActive(false);
            controls.SetActive(false);
            LockCursor();
        }
    }

    public void BackToPause()
    {
        settingsButtons.SetActive(true);
        backgroundSettings.ForEach(i => i.SetActive(false));
        audioButtons.SetActive(false);
        controls.SetActive(false);
    }

    public void Audio()
    {
        settingsButtons.SetActive(false);
        backgroundSettings.ForEach(i => i.SetActive(true));
        audioButtons.SetActive(true);
    }

    public void Controls()
    {
        settingsButtons.SetActive(false);
        backgroundSettings.ForEach(i => i.SetActive(true));
        controls.SetActive(true);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Editor.Quit();
        #endif
    }

    void LockCursor()
    {
        if(!menu)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    void FreeCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(!menu) Time.timeScale = 0;
    }
}
