using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] private float paddingLeft = 0.5f;
    [SerializeField] private float paddingRight = 0.5f;
    [SerializeField] private float paddingTop = 0.5f;
    [SerializeField] private float paddingBottom = 0.5f;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] public float moveTime = 1f;
    [SerializeField] public float pauseTime = 0.5f;

    private bool isInitializingPosition = true;

    private void Start() {
        InitBounds();
    }

    public void StartMoving() {
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine() {
        while (true) {
            if (isInitializingPosition) {
                isInitializingPosition = false;
                yield return new WaitForEndOfFrame();
            }

            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
            float timer = moveTime;

            while (timer > 0) {
                transform.position += randomDirection * moveSpeed * Time.deltaTime;
                ClampPosition();
                timer -= Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void InitBounds() {
        Camera camera = Camera.main;
        minBounds = camera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = camera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void ClampPosition() {
        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
