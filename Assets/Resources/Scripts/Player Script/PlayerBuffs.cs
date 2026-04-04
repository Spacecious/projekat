using UnityEngine;
using System.Collections;
public class PlayerBuffs : MonoBehaviour
{
    [SerializeField] int hitToHeal = 4;
    [SerializeField] float LuckyShotCooldown = 5f;

    private int hitCount = 0;
    private bool canLuckyShot = true;
    private PlayerController controls;
    private HealthComponent health;
    private SlotMachineUI slotUI;
    private PlayerMovement movement;
    private PlayerCombat combat;

    void Start()
    {
        controls = GetComponent<PlayerController>();
        health = GetComponent<HealthComponent>();
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
        slotUI = Object.FindAnyObjectByType<SlotMachineUI>();
    }

    void Update()
    {
       

        
        if (PlayerController.canUseLuckyShot && controls.gambleAction.triggered && canLuckyShot)
        {
            ActivateLuckyShot();
        }
    }

    public void RegisterHit()
    {
        hitCount++;
        if (hitCount >= hitToHeal)
        {
            health.Heal();
            hitCount = 0;
        }
    }

    void ActivateLuckyShot()
    {
        canLuckyShot = false;
        int s1 = Random.Range(0, 3), s2 = Random.Range(0, 3), s3 = Random.Range(0, 3);
        slotUI?.StartSpinning(s1, s2, s3);
        StartCoroutine(ApplyBuffWithDelay(s1, s2, s3, 1.0f));
        StartCoroutine(GambleCooldownRoutine());
    }

    IEnumerator ApplyBuffWithDelay(int s1, int s2, int s3, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (s1 == s2 && s2 == s3) ApplyBuffEffect(s1);
    }

    void ApplyBuffEffect(int type)
    {
        switch (type)
        {
            case 0: health.Heal(); break;
            case 1: StartCoroutine(DamageBuff(10f, 5f)); break;
            case 2: StartCoroutine(SpeedBuff(4f, 5f)); break;
        }
    }

    IEnumerator DamageBuff(float mult, float dur)
    {
        combat.damageMultiplier = mult;
        yield return new WaitForSeconds(dur);
        combat.damageMultiplier = 1f;
    }

    IEnumerator SpeedBuff(float mult, float dur)
    {
        movement.moveSpeed *= mult;
        yield return new WaitForSeconds(dur);
        movement.moveSpeed /= mult;
    }

    IEnumerator GambleCooldownRoutine()
    {
        yield return new WaitForSeconds(LuckyShotCooldown);
        canLuckyShot = true;
    }
}
