using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Level")]
    public int nextLvl = 2;
    public bool checkLvl;

    [Header("Rocket")]
    public KeyCode key = KeyCode.B;
    public GameObject warn;
    private bool onRadious;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if(Input.GetKeyDown(key) && warn.activeSelf && onRadious)
        {
            BackToMenu();
            SaveManager.Instance.SaveItems();
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            onRadious = true;
    }

    #region Rocket

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        SaveManager.Instance.SaveItems();
    }

    public void ShowWarn()
    {
        warn.SetActive(true);
    }

    #endregion

    #region  Level

    void Init()
    {
        if(SaveManager.Instance.setup.levelUnlocked >= nextLvl)
        {
            checkLvl = true;
            ShowWarn();
        }
    }

    public void NextLevel()
    {
        if(!checkLvl)
        {
            SaveManager.Instance.SaveNextLevel(nextLvl);
            ShowWarn();
        }
    }

    #endregion
}

