using UnityEngine;
public class StateRandom : StateMachineBehaviour
{
	static int AnimatorRandom = Animator.StringToHash("Random");
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetFloat(AnimatorRandom, Random.Range(0f, 1f));
	}
}
