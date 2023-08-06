using UnityEngine;

namespace PDA
{
    [CreateAssetMenu(menuName = "PDA/Simple Animator State")]
    public class SimplePDAnimatorStateData : PDAnimatorStateData
    {
        public PDAnimationClip clip;

        public override PDAnimatorState ConstructState(PDAnimator animator)
        {
            return new SimplePDAnimatorState(stateName, this);
        }
    }

    public class SimplePDAnimatorState : PDAnimatorState
    {
        new readonly SimplePDAnimatorStateData data;

        public SimplePDAnimatorState(string name, PDAnimatorStateData data) : base(name, data)
        {
            this.data = data as SimplePDAnimatorStateData;
        }

        public override void OnEnter(PDAnimator animator)
        {
            animator.SetCurrentAnimation(data.clip);
        }
    }
}
