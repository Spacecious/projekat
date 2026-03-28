using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    InputAction moveAction;
    InputAction fireAction;
    InputAction DashAction;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    Vector3 moveInput;

    private int Ammo=5;


    [SerializeField]
    GameObject projectile;

    [SerializeField] private float paddingLeft = 0.5f;
    [SerializeField] private float paddingRight = 0.5f;
    [SerializeField] private float paddingTop = 5f;
    [SerializeField] private float paddingBottom = 0.5f;

    float moveSpeed = 10.0f;

    private Boolean cd = true;
    private bool isReloading = false;


    [SerializeField] float blinkDistance = 5f; 
    [SerializeField] float blinkCooldown = 1.5f;
    
    private bool isDashing = false;
    private bool canBlink = true;

    [SerializeField] private int HitToHeal = 4;
    private int Count = 0;
    private HealthComponent playerHealth;

    private bool isStunned = false;

    void Awake() 
    {
        playerHealth = GetComponent<HealthComponent>();
    }

    public void RegisterHit()
    {
        Count++;
        Debug.Log("Pogodaka: " + Count);

        if (Count >= HitToHeal)
        {
            playerHealth.Heal(); 
            Count = 0;  
            Debug.Log("Pasivni Heal aktiviran!");
        }
    }
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Jump");
        DashAction = InputSystem.actions.FindAction("Dash");
        InitBounds();
    }

    private void InitBounds()
    {
        Camera camera = Camera.main;
        minBounds=camera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = camera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        if (isDashing || isStunned) return;
        MovePlayer();
        Shoot();
        HandleDashInput();
    }

    private void HandleDashInput()
    {
        if (DashAction.triggered && canBlink)
        {
            StartCoroutine(DashRoutine());
        }
    }

    IEnumerator DashRoutine()
    {
        canBlink = false;

        Vector2 blinkDir = moveAction.ReadValue<Vector2>().normalized;
        if (blinkDir.magnitude == 0) blinkDir = Vector2.up; 
        Vector3 targetPos = transform.position + (Vector3)blinkDir * blinkDistance;
        targetPos.x = Mathf.Clamp(targetPos.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        targetPos.y = Mathf.Clamp(targetPos.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = targetPos;
        yield return new WaitForSeconds(blinkCooldown);
        canBlink = true;
    }


    private void Shoot()
    {
            if (fireAction.triggered && cd == true)
            {
                GameObject projObj = Instantiate(projectile, transform.position, Quaternion.identity);
                Projectile proj = projObj.GetComponent<Projectile>();
                proj.setVelocity(new Vector2(0, 10), gameObject.tag);
                Ammo = Ammo - 1;
                Debug.Log(Ammo);
                if (Ammo <= 0)
                {

                    
                    StartCoroutine(Reload());
                }
            else
            {
                
                StartCoroutine(Cooldown());
            }
            cd = false;
            Destroy(projObj, 5f);
            }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        if (!isReloading) cd = true;
    }
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(5f);

        Ammo = 5;
        isReloading = false;
        cd = true; 
        Debug.Log("Reload zavr�en!");

    }

    private void MovePlayer()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        Vector3 newPos = transform.position + moveInput * (Time.deltaTime * moveSpeed);

        newPos.x = Mathf.Clamp(newPos.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);

        transform.position = newPos;
    }

    public void Slow(float slow)
    {
        moveSpeed = moveSpeed * 0.4f;
        StartCoroutine(Slows(slow));
    }
    IEnumerator Slows(float slow) {
        
        yield return new WaitForSeconds(slow);
        moveSpeed = 10f;
    }

    //Monakove metode nove za stun

    public void ApllyStun(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(StunRoutine(duration));
        }
    }

    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        Debug.Log("IGRAČ JE STUNOVAN!");

        yield return new WaitForSeconds(duration);

        isStunned = false;
        Debug.Log("Igrač se oporavio.");
    }
}
