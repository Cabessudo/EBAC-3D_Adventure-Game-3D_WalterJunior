using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anim
{
        public enum AnimType
        {
            NONE,
            IDLE,
            RUN,
            ATTACK,
            DEATH
        }
        
    public class AnimationBase : MonoBehaviour
    {

        public Animator anim;
        public List<AnimationSetup> animSetups; 

        public void SetAnimByType(AnimType currAnimType)
        {
            var setup = animSetups.Find(i => i.animType == currAnimType);
            if(setup != null)
                anim.SetTrigger(setup.animTrigger);
        }

        [System.Serializable]
        public class AnimationSetup
        {
            public AnimType animType;
            public string animTrigger;
        }
    }
}
