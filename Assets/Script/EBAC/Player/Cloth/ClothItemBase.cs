using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        private string playerTag = "Player";
        public float duration = 2;
        
        protected void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag(playerTag))
            {
                Collect();
            }
        }

        protected virtual void Collect()
        {
            Debug.Log("Cloth Collected: " + clothType.ToString());
            var setup = ClothManager.Instance.GetClothByType(clothType);
            EbacPlayer.Instance.ChangeCloth(setup.tex, duration);
            Invoke(nameof(HideItem), .1f);
        }

        void HideItem()
        {
            gameObject.SetActive(false);
        }
    }
}
