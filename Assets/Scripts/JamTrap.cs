using UnityEngine;

public class JamTrap : MonoBehaviour
{
    [SerializeField] float stunDuration = 2f;
    [SerializeField] float lifeTime = 5f;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            
            if (player != null)
            {
                player.ApplyStun(stunDuration);
            }

            Destroy(gameObject);
        }
    }
}
