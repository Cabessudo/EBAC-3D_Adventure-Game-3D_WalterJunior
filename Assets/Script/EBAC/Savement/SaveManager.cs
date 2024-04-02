using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using System.IO;
using System;
using Items;

public class SaveManager : Singleton<SaveManager>
{
    private string _path;
    private SaveSetup _saveSetup;
    // public Action<SaveSetup> fileLoaded;
    public SaveSetup setup
    {
        get{ return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        Load();
        // Invoke(nameof(Load), .1f)

    }
    
    public void SaveNextLevel(int lvl)
    {
        _saveSetup.levelUnlocked = lvl;
        SaveAll();
        Save();
    }

    public void SaveCloth(Texture tex)
    {
        _saveSetup.playerCloth = tex;
        Save();
    }

    public void SaveItems()
    {
        if(_saveSetup.coins < ItemManager.Instance.GetItemByType(ItemType.Coin).soInt.value)
            _saveSetup.coins = ItemManager.Instance.GetItemByType(ItemType.Coin).soInt.value;

        if(_saveSetup.lifePack < ItemManager.Instance.GetItemByType(ItemType.LifePack).soInt.value)    
            _saveSetup.lifePack = ItemManager.Instance.GetItemByType(ItemType.LifePack).soInt.value;

        Save();
    }

    public void SavePlayerHealth()
    {
        _saveSetup.playerHealth = EbacPlayer.Instance.playerHealth.currLife;
        Save();
    }

    public void SaveCheckpoints(Vector3 checkpoint)
    {
        _saveSetup.checkPointPos = checkpoint;
        Save();
    }

    public void SaveAll()
    {
        SavePlayerHealth();
        SaveItems();
    } 

    [NaughtyAttributes.Button]
    void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.playerName = "";
        _saveSetup.levelUnlocked = 1;
        _saveSetup.coins = 0;
        _saveSetup.lifePack = 0;
        _saveSetup.playerCloth = null;
        _saveSetup.playerHealth = 0;
        _saveSetup.checkPointPos = Vector3.zero;
        Save();
    }


    public void Save()
    {
        string json = JsonUtility.ToJson(_saveSetup);

        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
        Debug.Log(json);
    }

    public void Load()
    {
        _path = Application.persistentDataPath + "/save.txt";
        
        if(File.Exists(_path))
        {
            string json = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(json);
            Debug.Log(json);
            Debug.Log("Load");
        }
        else
        {
            CreateNewSave();
            Debug.Log("Creatre new save");
        }

        // fileLoaded?.Invoke(_saveSetup);
    }

    void OnDestroy()
    {
        SaveAll();
    }

    [System.Serializable]
    public class SaveSetup
    {
        //Player
        public string playerName;
        public float playerHealth;

        //Level
        public int levelUnlocked;

        //Items
        public int coins;
        public int lifePack;

        //Cloth
        public Texture playerCloth;

        //Checkpoints
        public Vector3 checkPointPos;
    }
}