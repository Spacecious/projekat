using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class firstBossAttack : StateMachineBehaviour
{
    
        PathFinding pf;
        private static int attackCount = 0; 

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           pf = animator.GetComponentInParent<PathFinding>();
        

        Rigidbody2D rb = animator.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        attackCount++;
        pf.Attack(attackCount);
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }
    
}
