using UnityEngine;

public class AppleTrn : MonoBehaviour
{

    [SerializeField] float speed = 8f;
    [SerializeField] int damage = 1;
    [SerializeField] float lifeTime = 1f;


    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            HealthComponent ClickyHealth = other.GetComponent<HealthComponent>();
            if(ClickyHealth != null)
            {
                ClickyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
