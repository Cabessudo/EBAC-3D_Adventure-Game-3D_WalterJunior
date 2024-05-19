using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anim
{
    public enum AnimPlayerType
    {
        NONE,
        IDLE,
        RUN,
        JUMP,
        FALL,
        LAND,
        DEATH
    }

    public class AnimationPlayer : AnimationBase<AnimPlayerType>
    {
        private AnimationBase<AnimPlayerType> animationBase;
    }
}
