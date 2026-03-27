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

        hc.TakeDamage(100);

        if (sender == "player")
        {
            Player player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
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
