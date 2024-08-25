using System;
using DG.Tweening;

namespace Builder.Utility.Animations
{
    public abstract class DoTweenAnimation : IDisposable
    {
        protected void PlayAnimation(Sequence currentAnimation, Sequence animation)
        {
            currentAnimation?.Complete(true);
            currentAnimation = animation;
            currentAnimation.Restart();
        }
        
        public abstract void Dispose();
    }
}