using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        protected ClothSetup _clothSetup;
        protected string playerTag = "Player";
        public float duration = 2;

        void Start()
        {
            _clothSetup = ClothManager.Instance.GetClothByType(clothType);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag(playerTag))
            {
                var player = other.gameObject.GetComponent<MyPlayer>();
                player?.StopAllCoroutines();
                Collect();
            }
        }

        protected virtual void Collect()
        {
            Debug.Log("Cloth Collected: " + clothType.ToString());
            Invoke(nameof(HideItem), .1f);
            OnCollect();
            ChangePWUP(_clothSetup.tex);
        
            SaveManager.Instance.SaveClothCollected(clothType);
        }
        
        void OnCollect()
        {
            if(SaveManager.Instance.setup.secondClothType == ClothType.NONE_Second && SaveManager.Instance.setup.firstClothType != ClothType.NONE_First)
            {
                SaveManager.Instance.SaveSecondCloth(_clothSetup);
                PlayerUI.Instance.CheckFirstCloth(false);
                PlayerUI.Instance.GetSecondCloth();
            }

            if(SaveManager.Instance.setup.firstClothType == ClothType.NONE_First)
            {
                SaveManager.Instance.SaveFirstCloth(_clothSetup);
                PlayerUI.Instance.CheckFirstCloth(true);
                PlayerUI.Instance.GetFirstCloth();
            }

            PlayerUI.Instance.ChangeMainCloth(_clothSetup.headSprite);            
        }

        void HideItem()
        {
            gameObject.SetActive(false);
        }


        public void ChangePWUP(Texture tex)
        {
            MyPlayer.Instance.ChangePwupCloth(tex, duration);
            ActivePWUP();
        }

        public virtual void ActivePWUP()
        {}
    }
}
