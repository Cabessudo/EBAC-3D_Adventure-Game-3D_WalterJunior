using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anim
{
    public enum AnimUIType
    {
        NONE,
        HIT, 
        OPEN_CHEST
    }

    public class AnimationUI : AnimationBase<AnimUIType>
    {
        private AnimationBase<AnimUIType> animationBase;

    }
}
