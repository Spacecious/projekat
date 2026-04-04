using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 10.0f;
    [SerializeField] float blinkDistance = 5f;
    [SerializeField] float blinkCooldown = 3f;

    [Header("Bounds Padding")]
    [SerializeField] float paddingLeft = 0.5f, paddingRight = 0.5f, paddingTop = 0.5f, paddingBottom = 0.5f;

    private Vector2 minBounds, maxBounds;
    private bool canBlink = true;
    private bool isStunned = false;
    private float originalMoveSpeed;
    private PlayerController controls;
    private AbilitiesCooldownUI abilityUI;

    void Start()
    {
        controls = GetComponent<PlayerController>();
        abilityUI = Object.FindAnyObjectByType<AbilitiesCooldownUI>();
        originalMoveSpeed = moveSpeed;
        InitBounds();
    }

    void Update()
    {
        if (isStunned) return;
        Move();
        if (PlayerController.firstBossDefeated && controls.dashAction.triggered && canBlink) StartCoroutine(DashRoutine());
    }

    private void Move()
    {
        Vector2 input = controls.moveAction.ReadValue<Vector2>();
        Vector3 newPos = transform.position + (Vector3)input * (Time.deltaTime * moveSpeed);
        newPos.x = Mathf.Clamp(newPos.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    private void InitBounds()
    {
        Camera cam = Camera.main;
        minBounds = cam.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = cam.ViewportToWorldPoint(new Vector2(1, 1));
    }

    IEnumerator DashRoutine()
    {
        canBlink = false;
        if (abilityUI != null) abilityUI.StartDashCooldown(blinkCooldown);
        Vector2 dir = controls.moveAction.ReadValue<Vector2>().normalized;
        if (dir.magnitude == 0) dir = Vector2.up;

        Vector3 targetPos = transform.position + (Vector3)dir * blinkDistance;
        targetPos.x = Mathf.Clamp(targetPos.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        targetPos.y = Mathf.Clamp(targetPos.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = targetPos;

        yield return new WaitForSeconds(blinkCooldown);
        canBlink = true;
    }

    public void Slow(float duration)
    {
        moveSpeed *= 0.4f;
        StartCoroutine(ResetSpeed(duration));
    }

    public void ApplyStun(float duration)
    {
        if (!isStunned) StartCoroutine(StunRoutine(duration));
    }

    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        moveSpeed = 0;
        yield return new WaitForSeconds(duration);
        isStunned = false;
        moveSpeed = originalMoveSpeed;
    }

    IEnumerator ResetSpeed(float d)
    {
        yield return new WaitForSeconds(d);
        moveSpeed = originalMoveSpeed;
    }
}
