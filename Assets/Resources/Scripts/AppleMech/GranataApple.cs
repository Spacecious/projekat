using UnityEngine;

public class GranataApple : MonoBehaviour
{

    [SerializeField] GameObject AppleThornPrefab;
    [SerializeField] float speed = 10f;
    [SerializeField] float timeToExplode = 1f;

    [SerializeField] int numberOfThorns = 8;

    private Transform Clicky;
    private bool hasExploded = false;

    
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.up * 10f;
        }
        Invoke("Explode", timeToExplode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        if (hasExploded)
            return;
        hasExploded = true;
        //Debug.Log("GRANATA BUM! Stvaram trnove.");

        float angleStep = 360f / numberOfThorns;
        float angle = 0f;

        for(int i = 0; i < numberOfThorns; i++)
        {
            Quaternion thornRotation = Quaternion.Euler(0, 0, angle);

            Instantiate(AppleThornPrefab, transform.position, thornRotation);

            angle += angleStep;
        }
        Destroy(gameObject);
    }
}
