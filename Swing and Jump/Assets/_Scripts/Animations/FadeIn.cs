using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : StateMachineBehaviour
{

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MySceneManager.Instance.LoadNextScene();
    }

}
