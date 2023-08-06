using System.Collections.Generic;

namespace PDA
{
    [UnityEngine.CreateAssetMenu(menuName = "PDA/Animator Template")]
    public class PDAnimatorTemplate : UnityEngine.ScriptableObject
    {
        public List<PDAnimatorStateData> states;
    }
}
