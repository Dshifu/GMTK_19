using UnityEngine;

public class PlayParticle : StateMachineBehaviour
{
    [SerializeField] public ParticleSystem Particle;

    private static GameObject currentMover;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    currentMover = Instantiate(Moover, Cotainer.transform);
    //    currentMover.GetComponent<MovementBonusEffect>().enabled = false;
    //    foreach (var collider in currentMover.GetComponentsInChildren<Collider>())
    //        collider.enabled = false;
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!Particle.isPlaying && animator.GetBool(PrefsName.AnimatorState.IsNeedToPlayParticle)) Particle.Play(true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
    //    Destroy(currentMover);

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
