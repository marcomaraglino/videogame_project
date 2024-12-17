using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;


public class BearAttackState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;

    public float stopAttackingDistance = 7.1f;

    public float attackRate = 0.5f;
    private float attackTimer;
    private int damageToInflict = 10;
    
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAtPlayer();
        if (attackTimer <= 0) {
            Attack();
            attackTimer = 1f / attackRate;
            
            
            
        } else {
            attackTimer -= Time.deltaTime;
            
           
        }
        
        //If agent should stop attacking
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer > stopAttackingDistance) {
            animator.SetBool("isAttacking", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void LookAtPlayer(){
        Vector3 direction = player.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void Attack(){  
       PlayerState.Instance.TakeDamage(damageToInflict);
       PostProcessVolume postProcessDamage = GameObject.FindWithTag("Damage").GetComponent<PostProcessVolume>();
       if(postProcessDamage.enabled == false)
       {
        postProcessDamage.enabled = true;
       }
       agent.GetComponent<MonoBehaviour>().StartCoroutine(DisablePostProcess(postProcessDamage));
    }

    private IEnumerator DisablePostProcess(PostProcessVolume postProcessDamage) {
        // Inizializza il valore di dissolvenza
        float duration = 0.6f; // Durata della dissolvenza
        float elapsedTime = 0f;

        // Supponiamo che il PostProcessVolume abbia un effetto di vignettatura
        Vignette vignette = postProcessDamage.profile.GetSetting<Vignette>();
        if (vignette != null) {
            // Dissolvi l'effetto
            while (elapsedTime < duration) {
                elapsedTime += Time.deltaTime;
                vignette.intensity.value = Mathf.Lerp(0.6f, 0f, elapsedTime / duration); // Dissolvi da 1 a 0
                yield return null; // Aspetta il prossimo frame
            }
        }

        postProcessDamage.enabled = false; // Disattiva il post-process
    }
    
    
}
