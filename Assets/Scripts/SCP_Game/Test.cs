using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Test : MonoBehaviour
{
    public bool grounded;

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    
}

/* Se Precisar
namespace Level
{
    public enum LevelType
    {
        First,
        Second,
        Third
    }

    public class LevelManager : Singleton<LevelManager>
    {
        public List<LevelSetup> lvlSetup;

        public LevelSetup GetLevelSetupByType(LevelType levelType)
        {
            return lvlSetup.Find(i => i.lvlType == levelType);
        }

        [System.Serializable]
        public class LevelSetup
        {
            public LevelType lvlType;
            public Texture leveltex;
        }
    }
}
*/
