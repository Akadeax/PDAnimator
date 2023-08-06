using System.Collections.Generic;
using UnityEngine;

namespace PDA
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PDAnimator : MonoBehaviour
    {
        [SerializeField] PDAnimatorTemplate template;
        readonly Dictionary<string, PDAnimatorState> stateList = new();
        SpriteRenderer rend;

        PDAnimatorState currentState;
        float timeSinceStateEnter = 0f;
        [HideInInspector] public float animationSpeed = 1f;

        PDAnimationClip currentAnimation;

        private void Awake()
        {
            rend = GetComponent<SpriteRenderer>();

            if (template == null)
            {
                Debug.LogError("PDAnimator does not have template.");
                return;
            }

            if (template.states.Count != 0)
            {
                // Construct all states
                foreach (PDAnimatorStateData data in template.states)
                {
                    stateList.Add(data.stateName, data.ConstructState(this));
                }

                currentState = stateList[template.states[0].stateName];
                currentState.OnEnter(this);
            }
        }

        private void Update()
        {
            if (currentState == null) return;

            currentState.OnUpdate(this);

            if (currentAnimation == null || currentAnimation.IsEmpty) return;

            float singleSpriteDuration = currentAnimation.duration / currentAnimation.sprites.Count;
            int animationIndex = (int)Mathf.Clamp(timeSinceStateEnter / singleSpriteDuration, 0, currentAnimation.sprites.Count - 1);
            rend.sprite = currentAnimation.sprites[animationIndex];

            timeSinceStateEnter += Time.deltaTime * animationSpeed;
            if (currentAnimation.isLooping && timeSinceStateEnter > currentAnimation.duration)
            {
                timeSinceStateEnter = 0f;
            }
        }

        public void SwitchState(string newState)
        {
            if (!stateList.ContainsKey(newState))
            {
                Debug.LogWarning($"Could not find state '{newState}'");
                return;
            }

            currentState.OnExit(this);
            currentState = stateList[newState];
            timeSinceStateEnter = 0f;
            currentState.OnEnter(this);
        }

        public void SetCurrentAnimation(PDAnimationClip clip)
        {
            if (clip == null) Debug.LogWarning($"Trying to set animation clip on {gameObject.name} to null.");
            currentAnimation = clip;
        }
    }
}
