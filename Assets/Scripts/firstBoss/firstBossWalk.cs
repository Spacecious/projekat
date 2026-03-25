using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class firstBossWalk : StateMachineBehaviour
{
    PathFinding pf;

    [SerializeField] float attackCooldown = 3.0f;
    float cooldownTimer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        pf = animator.GetComponent <PathFinding>();
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (pf.player == null) return;

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        float dist = Mathf.Abs(pf.player.position.x - pf.transform.position.x);

        if (dist <= pf.stopDistance && cooldownTimer <= 0)
        {
            animator.SetTrigger("Attack");
            cooldownTimer = attackCooldown;
            pf.speed = 0f;
        }
        else
        {
            pf.MoveTowardsPlayer();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    


}
