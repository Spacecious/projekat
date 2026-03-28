using UnityEngine;

public class VineTrap : MonoBehaviour
{

    [SerializeField] float stunDuration = 1f;
    [SerializeField] float lifeTime = 4f;

    
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.ApllyStun(stunDuration);
            }

            Destroy(gameObject);
        }
    }
}
