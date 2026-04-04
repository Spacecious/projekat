using UnityEngine;

public class JamTrap : MonoBehaviour
{
    [SerializeField] float slowDuration = 2f;
    [SerializeField] float lifeTime = 5f;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            
            if (player != null)
            {
                player.Slow(slowDuration);
            }

            Destroy(gameObject);
        }
    }
}
