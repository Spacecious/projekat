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
    InputAction gambleAction;

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
    [SerializeField] float blinkCooldown = 3f;

    [SerializeField] private float gambleCooldown = 10f;
    
    private bool isDashing = false;
    private bool canBlink = true;

    [SerializeField] private int HitToHeal = 4;
    private int Count = 0;
    private HealthComponent playerHealth;


    public float damageMultiplier = 1f;
    //private bool isSpeedBuffed = false;
    private float originalMoveSpeed;
    private bool canGamble = true;

    private SlotMachineUI slotUI;
    private AmmoUI ammoUI;

    private AbilitiesCooldownUI abilityUI;

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
        gambleAction = InputSystem.actions.FindAction("Gamble");
        originalMoveSpeed = moveSpeed;
        InitBounds();
        slotUI = GameObject.FindFirstObjectByType<SlotMachineUI>();
        ammoUI = GameObject.FindFirstObjectByType<AmmoUI>();
        abilityUI = GameObject.FindFirstObjectByType<AbilitiesCooldownUI>();
        if (abilityUI == null)
    {
        Debug.LogError("CRTIČNA GREŠKA: PlayerMovement nije našao AbilityCooldownUI u sceni! Proveri da li je skripta dodata na Canvas.");
    }
    else 
    {
        Debug.Log("Uspešno povezan Ability Cooldown UI!");
    }
        UpdateUI();
    }

    private void InitBounds()
    {
        Camera camera = Camera.main;
        minBounds=camera.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = camera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        if (isDashing) return;
        MovePlayer();
        Shoot();
        HandleDashInput();
        if (gambleAction != null && gambleAction.triggered)
        {
            ActivateGamble();
        }
    }

    private void HandleDashInput()
    {
        if (DashAction.triggered && canBlink)
        {
            StartCoroutine(DashRoutine());
        }
    }
    void UpdateUI()
    {
        if (ammoUI != null) ammoUI.UpdateAmmoDisplay(Ammo);
    }

    IEnumerator DashRoutine()
    {
        canBlink = false;
        if (abilityUI != null) abilityUI.StartDashCooldown(blinkCooldown);
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
                UpdateUI();
               Debug.Log("Pucaj! Preostalo: " + Ammo);
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
        Debug.Log("Reload zavrsen!");

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


    private void ActivateGamble()
    {

        if (!canGamble) return;
        // Simuliramo 3 slota (0 = Limun, 1 = Sedmica, 2 = Borovnica)
        int slot1 = UnityEngine.Random.Range(0, 3);
        int slot2 = UnityEngine.Random.Range(0, 3);
        int slot3 = UnityEngine.Random.Range(0, 3);

        Debug.Log($"SLOT: {slot1} | {slot2} | {slot3}");

        if (slotUI != null)
    {
        slotUI.StartSpinning(slot1, slot2, slot3);
    }

    StartCoroutine(ApplyBuffWithDelay(slot1, slot2, slot3, 1.0f)); 
    if (abilityUI != null) abilityUI.StartGambleCooldown(gambleCooldown);
        StartCoroutine(GambleCooldown());
    }

    IEnumerator ApplyBuffWithDelay(int s1, int s2, int s3, float delay)
{
    yield return new WaitForSeconds(delay); // Čekamo da se slotovi "zaustave"
    
    if (s1 == s2 && s2 == s3)
    {
        ApplyBuff(s1); // Tvoja stara funkcija koja pali Shield, Damage ili Speed
    }
    else
        {
            Debug.Log("Vise srece drugi put!");
        }
}

    IEnumerator GambleCooldown()
    {
        canGamble = false;
        yield return new WaitForSeconds(10f); 
        canGamble = true;
        Debug.Log("Gamble spreman ponovo!");
    }
    private void ApplyBuff(int type)
    {
        switch (type)
        {
            case 0: // 3 LIMUNA = Heal
                Debug.Log("Healed!!!");
                GetComponent<HealthComponent>().Heal(); 
                break;

            case 1: // 3 SEDMICE = Damage x3
                StartCoroutine(DamageBuffRoutine(3f, 5f)); // x3 damage na 5 sekundi
                break;

            case 2: // 3 BOROVNICE = Speed x2
                StartCoroutine(SpeedBuffRoutine(2f, 5f)); // x2 brzina na 5 sekundi
                break;
        }
    }
    IEnumerator DamageBuffRoutine(float multiplier, float duration)
    {
        damageMultiplier = multiplier;
        Debug.Log("DAMAGE BUFF ON!");
        yield return new WaitForSeconds(duration);
        damageMultiplier = 1f;
        Debug.Log("Damage back to normal.");
    }

    IEnumerator SpeedBuffRoutine(float multiplier, float duration)
    {
        moveSpeed = originalMoveSpeed * multiplier;
        Debug.Log("SPEED BUFF ON!");
        yield return new WaitForSeconds(duration);
        moveSpeed = originalMoveSpeed;
        Debug.Log("Speed back to normal.");
    }
}
