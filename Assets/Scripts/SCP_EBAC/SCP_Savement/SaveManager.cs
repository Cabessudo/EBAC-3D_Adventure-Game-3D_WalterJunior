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
    private SaveSetup _saveSetup;
    
    public SaveSetup setup
    {
        get{ return _saveSetup; }
    }

    public ClothSetup firstCloth;
    public ClothSetup secondCloth;

    #region Start Game
    protected override void Awake()
    {
        base.Awake();
        Load();
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        firstCloth = ClothManager.Instance.GetClothByType(_saveSetup.firstClothType);
        secondCloth = ClothManager.Instance.GetClothByType(_saveSetup.secondClothType);
    }
    #endregion
    
    public void SaveNextLevel(int lvl)
    {
        _saveSetup.levelUnlocked = lvl;
        Save();
    }

    public void SaveItems()
    {
        if(ItemManager.Instance != null)
        {
            if(_saveSetup.coins < ItemManager.Instance.GetItemByType(ItemType.Coin).itemAmount)
                _saveSetup.coins = ItemManager.Instance.GetItemByType(ItemType.Coin).itemAmount;

            if(_saveSetup.lifePack < ItemManager.Instance.GetItemByType(ItemType.LifePack).itemAmount)    
                _saveSetup.lifePack = ItemManager.Instance.GetItemByType(ItemType.LifePack).itemAmount;

            Save();
        }
    }

    public void SaveCheckpoints(Vector3 checkpoint)
    {
        _saveSetup.checkPointPos = checkpoint;
        Save();
    }

    public void SavePlayerHealth()
    {
        if(MyPlayer.Instance != null)
        {
            _saveSetup.playerHealth = MyPlayer.Instance.health.currLife;
            Save();
        }

    }

    #region Camera Sens
    public void SaveCameraSensX(float currSens)
    {
        _saveSetup.xSens = currSens;
        Save();
    }

    public void SaveCameraSensY(float currSens)
    {
        _saveSetup.ySens = currSens;
        Save();
    }
    #endregion

    #region Audio
    public void SaveMusic(int currVolume)
    {
        _saveSetup.music = currVolume;
        Save();
    }

    public void SaveSFX(int currVolume)
    {
        _saveSetup.sfx = currVolume;
        Save();
    }
    #endregion

    #region Cloths & Jetpack
    public void SaveClothCollected(ClothType cloth)
    {
        if(!_saveSetup.clothsUnlockeds.Contains(cloth))
            _saveSetup.clothsUnlockeds.Add(cloth);

        Save();
    }


    public void SaveFirstCloth(ClothSetup cloth)
    {
        if(firstCloth != cloth)
        {
            firstCloth = cloth;
            _saveSetup.firstClothType = cloth.type;
            Save();
            Init();
        }

    }

    public void SaveSecondCloth(ClothSetup cloth)
    {
        if(secondCloth != cloth)
        {
            secondCloth = cloth;
            _saveSetup.secondClothType = cloth.type;
            Save();
            Init();
        }
    }

    public void SaveJetpack()
    {
        _saveSetup.jetpackActive = true;
        Save();
    }
    #endregion

    #region Save & Load 

    [NaughtyAttributes.Button]
    public void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.playerName = "";
        _saveSetup.levelUnlocked = 1;
        _saveSetup.coins = 0;
        _saveSetup.lifePack = 0;
        _saveSetup.playerHealth = 0;
        _saveSetup.xSens = 150;
        _saveSetup.ySens = 2;
        _saveSetup.checkPointPos = Vector3.zero;
        _saveSetup.firstClothType = ClothType.NONE_First;
        _saveSetup.secondClothType = ClothType.NONE_Second;
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
    }

    #endregion

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

        //Audio
        public int music;
        public int sfx;

        //Camera Sensivity
        public float xSens;
        public float ySens;

        //Cloth PWUPS
        public List<ClothType> clothsUnlockeds;
        public ClothType firstClothType;
        public ClothType secondClothType;

        //Jetpack
        public bool jetpackActive;

        //Checkpoints
        public Vector3 checkPointPos;
    }
}