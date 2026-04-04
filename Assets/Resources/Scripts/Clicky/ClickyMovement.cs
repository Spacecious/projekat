using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickyMovement : MonoBehaviour
{
    [Header("Waypoint Settings")]
    [SerializeField] private int waypointCount = 50;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float waypointReachDistance = 0.2f;

    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashChance = 0.1f;

    [Header("Bounds")]
    [SerializeField] private float paddingLeft = 0.5f;
    [SerializeField] private float paddingRight = 0.5f;
    [SerializeField] private float paddingTop = 0.5f;
    [SerializeField] private float paddingBottom = 0.5f;

    [Header("Shooting")]
    [SerializeField] private ShootAtPlayer shooter;

    private Vector2 minBounds;
    private Vector2 maxBounds;
    private List<Vector3> waypoints = new List<Vector3>();
    private int currentWaypointIndex = 0;
    private bool isDashing = false;

    void Start() {
        InitBounds();
        GenerateWaypoints();
        StartCoroutine(MoveRoutine());

        shooter = GetComponent<ShootAtPlayer>();
        if (shooter != null) shooter.StartShooting();
    }

    private void InitBounds() {
        Camera camera = Camera.main;
        minBounds = camera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = camera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void GenerateWaypoints() {
        waypoints.Clear();
        Vector3 current = transform.position;

        for (int i = 0; i < waypointCount; i++) {
            float maxStep = 5f;
            float x = Mathf.Clamp(current.x + Random.Range(-maxStep, maxStep),
                minBounds.x + paddingLeft, maxBounds.x - paddingRight);
            float y = Mathf.Clamp(current.y + Random.Range(-maxStep, maxStep),
                minBounds.y + paddingBottom, maxBounds.y - paddingTop);

            current = new Vector3(x, y, 0);
            waypoints.Add(current);
        }
    }

    IEnumerator MoveRoutine() {
        while (true) {
            if (currentWaypointIndex >= waypoints.Count) {
                GenerateWaypoints();
                currentWaypointIndex = 0;
            }

            Vector3 target = waypoints[currentWaypointIndex];

            if (!isDashing && Random.value < dashChance) {
                yield return StartCoroutine(DashRoutine(target));
            }
            else {
                while (Vector3.Distance(transform.position, target) > waypointReachDistance) {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        target,
                        moveSpeed * Time.deltaTime
                    );
                    yield return null;
                }
            }

            currentWaypointIndex++;
        }
    }

    IEnumerator DashRoutine(Vector3 target) {
        isDashing = true;

        Vector3 dashDir = (target - transform.position).normalized;
        Vector3 dashTarget = transform.position + dashDir * dashDistance;

        dashTarget.x = Mathf.Clamp(dashTarget.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        dashTarget.y = Mathf.Clamp(dashTarget.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        transform.position = dashTarget;

        yield return new WaitForSeconds(0.1f);
        isDashing = false;
    }
}
