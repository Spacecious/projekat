using UnityEngine;

public class Granata : MonoBehaviour
{
    
    [SerializeField] GameObject thornPrefab;

    [SerializeField] float speed = 10f;
    [SerializeField] float timeToExplode = 1f;
    
    [SerializeField] int numberOfThorns = 8;

    private Transform player;
    private bool hasExploded = false;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if(playerObj != null)
        {
            player = playerObj.transform;
        }
        Invoke("Explode", timeToExplode);
    }

    
    void Update()
    {
        if(player != null && !hasExploded)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
        }

        float DistanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (DistanceToPlayer <= 3f)
        {
            Explode();
        }
    }

    void Explode()
    {
        if(hasExploded)
            return;
        hasExploded = true;
        Debug.Log("GRANATA BUM! Stvaram trnove.");

        float angleStep = 360f / numberOfThorns;
        float angle = 0f;

        for(int i = 0; i < numberOfThorns; i++)
        {
            Quaternion thornRotation = Quaternion.Euler(0, 0, angle);

            Instantiate(thornPrefab, transform.position, thornRotation);

            angle += angleStep;
        }
        Destroy(gameObject);
    }
}
