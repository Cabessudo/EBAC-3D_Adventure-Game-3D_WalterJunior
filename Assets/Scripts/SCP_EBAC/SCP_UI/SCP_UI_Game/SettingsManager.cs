using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
    using UnityEditor;
#endif

public class SettingsManager : MonoBehaviour
{
    public PauseManager pause;
    public GameObject mainBackground; 
    public GameObject pauseButtons;
    public GameObject settings;
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
            pauseButtons.SetActive(true);
            mainBackground.SetActive(true);
            showedSettings = true;
            FreeCursor();
            pause?.Enter();
        }
        else
        {
            pauseButtons.SetActive(false);
            mainBackground.SetActive(false);
            showedSettings = false;
            settings.SetActive(false);
            LockCursor();
            pause?.Exit();
        }
    }

    public void BackToPause()
    {
        pauseButtons.SetActive(true);
        settings.SetActive(false);
    }

    public void Settings()
    {
        pauseButtons.SetActive(false);
        settings.SetActive(true);
    }

    public void Controls()
    {
        pauseButtons.SetActive(false);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
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
