using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PDA
{
    [CreateAssetMenu(menuName = "PDA/Animation Clip 4D")]
    public class PDAnimationClip4D : ScriptableObject
    {
        public PDAnimationClip up;
        public PDAnimationClip right;
        public PDAnimationClip down;
        public PDAnimationClip left;

        public PDAnimationClip GetAppropriate(Vector2 direction, float upDownPointMultiplier = 0.75f)
        {
            // TODO: fix this, aka, goddamnit
            float distanceUp = Vector2.Distance(direction, new Vector2(0, upDownPointMultiplier));
            float distanceRight = Vector2.Distance(direction, new Vector2(1, 0));
            float distanceDown = Vector2.Distance(direction, new Vector2(0, -upDownPointMultiplier));
            float distanceLeft = Vector2.Distance(direction, new Vector2(-1, 0));

            float minDistance = Mathf.Min(distanceUp, distanceRight, distanceDown, distanceLeft);

            if (minDistance == distanceUp) return up;
            else if (minDistance == distanceRight) return right;
            else if (minDistance == distanceDown) return down;
            else return left;
        }
    }
}
