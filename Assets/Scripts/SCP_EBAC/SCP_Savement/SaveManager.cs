using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;
using System.IO;
using System;
using Items;
using Cloth;

public class SaveManager : Singleton<SaveManager>
{
    private string _path;
    private int clothsSelecteds;
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
        Init();
    }

    void Init()
    {
        clothsSelecteds = setup.currCloths.Count;
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
        _saveSetup.playerHealth = MyPlayer.Instance.health.currLife;
        Save();
    }

    public void SaveCheckpoints(Vector3 checkpoint)
    {
        _saveSetup.checkPointPos = checkpoint;
        Save();
    }

    public void SaveClothCollected(ClothItemBase cloth)
    {
        if(!_saveSetup.clothsUnlockeds.Contains(cloth))
            _saveSetup.clothsUnlockeds.Add(cloth);

        Save();
    }

    public void SaveClothsSelexteds(ClothItemBase cloth)
    {
        if(!_saveSetup.currCloths.Contains(cloth) && clothsSelecteds < 2)
        {
            _saveSetup.currCloths.Add(cloth);
            clothsSelecteds = setup.currCloths.Count;
        }

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
        _saveSetup.clothsUnlockeds.Clear();
        _saveSetup.currCloths.Clear();
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

            //PWUPS
            public List<ClothItemBase> clothsUnlockeds;
            public List<ClothItemBase> currCloths;

        //Checkpoints
        public Vector3 checkPointPos;
    }
}