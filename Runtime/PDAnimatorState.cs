namespace PDA
{
    public abstract class PDAnimatorStateData : UnityEngine.ScriptableObject
    {
        public string stateName;
        public abstract PDAnimatorState ConstructState(PDAnimator animator);
    }

    public abstract class PDAnimatorState
    {
        public string name;
        public PDAnimatorStateData data;
        public PDAnimatorState(string name, PDAnimatorStateData data)
        {
            this.name = name;
            this.data = data;
        }

        public virtual void OnEnter(PDAnimator animator) { }
        public virtual void OnUpdate(PDAnimator animator) { }
        public virtual void OnExit(PDAnimator animator) { }
    }

    // State Class Template:
    /*
    [CreateAssetMenu(menuName = "PD/")]
    public class StateData : PDAnimatorStateData
    {
        //[Header("Animations")]


        public override PDAnimatorState ConstructState(PDAnimator animator)
        {
            return new State(stateName, this, animator);
        }
    }

    public class State : PDAnimatorState
    {
        new StateData data;
        PDAnimator animator;

        public State(string name, PDAnimatorStateData data, PDAnimator animator) : base(name, data)
        {
            this.data = data as StateData;

        }
    }
    */
}
