using UnityEngine;

public class VineTrap : MonoBehaviour
{
    [SerializeField] float stunDuration = 1f;
    [SerializeField] float lifeTime = 4f;

    void Start()
    {
       
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerMovement movement = other.GetComponent<PlayerMovement>();

            if (movement != null)
            {
                
                movement.ApplyStun(stunDuration);
                Debug.Log("Igrac se upetljao u lozu! Stun trajanje: " + stunDuration + "s");
            }

          
            Destroy(gameObject);
        }
    }
}