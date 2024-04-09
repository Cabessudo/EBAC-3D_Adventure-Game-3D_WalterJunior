using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        private string playerTag = "Player";
        public bool pwupCloth = true;
        public float duration = 2;
        public bool canCollect = true;
        
        protected void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag(playerTag) && canCollect)
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            Debug.Log("Cloth Collected: " + clothType.ToString());
            var setup = ClothManager.Instance.GetClothByType(clothType);
            Invoke(nameof(HideItem), .1f);
            OnCollect(setup.tex);
            SaveManager.Instance.SaveClothCollected(this);
        }

        void OnCollect(Texture tex)
        {
            if(pwupCloth)
                MyPlayer.Instance.ChangePwupCloth(tex, duration);
            else
            {
                MyPlayer.Instance.ChangeCloth(tex);
                SaveManager.Instance.SaveCloth(tex);
                LevelManager.Instance.NextLevel();
                CameraManager.Instance.FocusOnRocket();
            }
        }

        void HideItem()
        {
            gameObject.SetActive(false);
        }

        public virtual void ActivePWUP()
        {
            var setup = ClothManager.Instance.GetClothByType(clothType);
            MyPlayer.Instance.ChangePwupCloth(setup.tex, duration);
        }
    }
}
