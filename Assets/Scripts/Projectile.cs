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
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (sender == "" || collision.CompareTag(sender))
            return;

        HealthComponent hc = collision.GetComponent<HealthComponent>();

        if (hc == null) return;


        Player playerr = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        int finalDamage = Mathf.RoundToInt(1 * playerr.damageMultiplier); 
        hc.TakeDamage(finalDamage);


        KupinaBoss boss = collision.GetComponent<KupinaBoss>();
        if(boss != null && boss.isShieldActive)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if(playerObj != null)
            {
                HealthComponent playerHealth = playerObj.GetComponent<HealthComponent>();
                if(playerHealth != null)
                {
                    playerHealth.TakeDamage((int)boss.GetReflectDamage());
                    Debug.Log("THORNS! Igrač je primio reflect damage!");
                }
            }
        }

        if (sender == "Player")
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (player != null) {
                player.RegisterHit();
            }
        }

        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
