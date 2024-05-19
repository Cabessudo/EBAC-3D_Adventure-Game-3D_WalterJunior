using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anim
{
    public enum AnimEnemyType
    {
        NONE,
        IDLE,
        RUN,
        ATTACK,
        STUNNED,
        DEATH
    }

    public class AnimationEnemy : AnimationBase<AnimEnemyType>
    {
        private AnimationBase<AnimEnemyType> animationBase;
    }
}
