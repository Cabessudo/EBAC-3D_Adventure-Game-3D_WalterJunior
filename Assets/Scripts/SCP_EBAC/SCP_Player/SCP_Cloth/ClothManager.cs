using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Singleton;

namespace Cloth
{
    public enum ClothType
    {
        NONE_First,
        NONE_Second,
        SPEED, 
        STRONG,
        HEAT,
        SHOOTGUN
    }


    public class ClothManager : Singleton<ClothManager>
    {
        public List<ClothSetup> clothSetups;
        
        public ClothSetup GetClothByType(ClothType clothType)
        {
            return clothSetups.Find(i => i.type == clothType);
        } 
    }

    [System.Serializable]
    public class ClothSetup
    {
        public ClothType type;
        public Texture2D tex;
        public ClothItemBase cloth;
        public Sprite clothSprite;
        public Sprite headSprite;
    }
}