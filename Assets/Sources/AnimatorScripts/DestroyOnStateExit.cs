using UnityEngine;

public class DestroyOnStateExit : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent<IDestroyable>(out var destroyable)) { destroyable.Destroy(); }
        else { Destroy(animator.gameObject); };
    }
}
