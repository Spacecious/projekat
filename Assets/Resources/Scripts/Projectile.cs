using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    private string sender = "";

    public void setVelocity(Vector2 velocity, string sender)
    {
        rb.linearVelocity = velocity;
        this.sender = sender;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (sender == "" || collision.CompareTag(sender))
            return;

    
        HealthComponent hc = collision.GetComponent<HealthComponent>();
        if (hc == null) return;

      
        int finalDamage = 1;

        if (sender == "Player")
        {
   
            PlayerCombat combat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
            if (combat != null)
            {
                finalDamage = Mathf.RoundToInt(1 * combat.damageMultiplier);
            }

            
            PlayerBuffs buffs = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBuffs>();
            if (buffs != null)
            {
                buffs.RegisterHit();
            }
        }

        
        hc.TakeDamage(finalDamage);

        
        Destroy(gameObject);
    }
}