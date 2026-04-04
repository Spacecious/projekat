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
         
            PlayerMovement movement = other.GetComponent<PlayerMovement>();

            if (movement != null)
            {
                movement.Slow(slowDuration);
                Debug.Log("Igrac je upao u dzem! Usporen na " + slowDuration + "s");
            }

            
            Destroy(gameObject);
        }
    }
}