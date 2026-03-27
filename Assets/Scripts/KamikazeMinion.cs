using System;
using Unity.VisualScripting;
using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    
    [SerializeField] float MinionSpeed = 5f;
    [SerializeField] float TimeBeforeExplosion = 3f;
    [SerializeField] float Damage = 10f;
    [SerializeField] float ExplosionRadius = 2f;

    private Transform player;
    private bool HasExploded = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        Invoke("Explode", TimeBeforeExplosion);
    }

    
    void Update()
    {
        if(player != null && !HasExploded)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, MinionSpeed*Time.deltaTime);

            float DistanceToPlayer = Vector2.Distance(transform.position, player.position);

            if(DistanceToPlayer <= ExplosionRadius)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        if(HasExploded)
            return;
        HasExploded = true;
        CancelInvoke("Explode");
        
        Debug.Log("BOOM!");
        Collider2D[] ObjectsInRange = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);

        foreach(Collider2D obj in ObjectsInRange)
        {
            HealthComponent health = obj.GetComponent<HealthComponent>();
            if(health != null && obj.CompareTag("Player"))
            {
                health.TakeDamage(Damage);
            }
        }
        Destroy(gameObject);
    }
}
