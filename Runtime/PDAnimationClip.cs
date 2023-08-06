using System.Collections.Generic;

namespace PDA
{
    [UnityEngine.CreateAssetMenu(menuName = "PDA/Animation Clip")]
    public class PDAnimationClip : UnityEngine.ScriptableObject
    {
        public float duration = 1f;
        public List<UnityEngine.Sprite> sprites = new();
        public bool isLooping = true;

        public bool IsEmpty
        {
            get
            {
                return sprites.Count == 0;
            }
        }
    }
}
