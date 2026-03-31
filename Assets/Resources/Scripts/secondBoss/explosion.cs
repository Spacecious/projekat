using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration = 0.5f;
    public float maxScale = 3f;
    private float timer = 0f;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        float progress = timer / duration; // 0 to 1

        // Scale up
        float scale = Mathf.Lerp(0f, maxScale, progress);
        transform.localScale = new Vector3(scale, scale, scale);

        // Fade out
        Color c = sr.color;
        c.a = Mathf.Lerp(1f, 0f, progress);
        sr.color = c;

        if (timer >= duration)
            Destroy(gameObject);
    }
}
