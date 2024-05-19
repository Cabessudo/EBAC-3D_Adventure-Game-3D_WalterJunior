using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anim
{       
    public class AnimationBase<T>: MonoBehaviour where T : System.Enum
    {
        public Animator anim;
        public List<AnimationSetup> animSetups;

        public void SetAnimByType(T currAnimType)
        {
            var setup = animSetups.Find(i => i.animType.ToString() == currAnimType.ToString());
            if(setup != null)
                anim.SetTrigger(setup.animTrigger);
        }
        
        public void SetAnimByType(T currAnimType, bool b)
        {
            var setup = animSetups.Find(i => i.animType.ToString() == currAnimType.ToString());
            if(setup != null)
                anim.SetBool(setup.animTrigger, b);
        }

        [System.Serializable]
        public class AnimationSetup
        {
            public T animType;
            public string animTrigger;
        }
    }
}
