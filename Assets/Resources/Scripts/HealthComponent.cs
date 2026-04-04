using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{

    [SerializeField] int MaxHealth = 3;
    private GameObject[] heartSprites;
    public int CurrentHealth;
    public AudioSource dmg;
    private EnemyHealthUI enemyUI;

    public UnityEvent<float> OnHealthChanged;

    public int GetHealth()
    {
        return CurrentHealth;
    }

    void Start()
    {
        CurrentHealth = MaxHealth;

        FindHeartsInScene();
        UpdateHeartUI();

        enemyUI = GetComponentInChildren<EnemyHealthUI>();

       
        if (enemyUI != null)
        {
            enemyUI.SetHealth(CurrentHealth, MaxHealth);
        }
    }

    void FindHeartsInScene()
    {
        // Tražimo srca samo ako je ovo igrač
        if (!gameObject.CompareTag("Player")) return;

        heartSprites = new GameObject[MaxHealth];

        for (int i = 0; i < MaxHealth; i++)
        {
            string heartName = "Health_" + (i + 1);
            heartSprites[i] = GameObject.Find(heartName);

            if (heartSprites[i] == null)
            {
                Debug.LogWarning("Nisam našao " + heartName + " u sceni!");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        if (dmg != null)
        {
            dmg.pitch = Random.Range(0.8f, 1.2f);
            dmg.Play();
        }

        if (gameObject.CompareTag("Player"))
        {
            UpdateHeartUI();

            
            PlayerBuffs buffs = GetComponent<PlayerBuffs>();
            if (buffs != null) buffs.RegisterHit();
        }

        if (enemyUI != null)
        {
            enemyUI.SetHealth(CurrentHealth, MaxHealth);
        }

        if (CurrentHealth <= 0)
        {
            if (gameObject.CompareTag("Boss"))
            {
                UnlockPlayerAbilities();
            }

            if (gameObject.CompareTag("Player"))
            {
                Debug.Log("Player je poginuo!");
            }

            Destroy(gameObject);
        }
    }

    private void UnlockPlayerAbilities()
    {
        

        // Tražimo PlayerController umesto stare Player skripte
        PlayerController pc = Object.FindAnyObjectByType<PlayerController>();
        if (pc != null)
        {
            if (pc.dashIcon != null) pc.dashIcon.SetActive(true);
            if (pc.gambleIcon != null) pc.gambleIcon.SetActive(true);
            if (pc.slotUI != null) pc.slotUI.gameObject.SetActive(true);
        }

        Debug.Log("BOSS PORAŽEN! Sposobnosti otključane.");
    }

    public void Heal()
    {
        CurrentHealth = Mathf.Min(CurrentHealth + 1, MaxHealth);
        UpdateHeartUI();
    }

    public void Heal(int health)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + health, MaxHealth);
        UpdateHeartUI();
    }

    void UpdateHeartUI()
    {
        if (heartSprites == null || heartSprites.Length == 0) return;

        for (int i = 0; i < heartSprites.Length; i++)
        {
            if (heartSprites[i] != null)
            {
                heartSprites[i].SetActive(i < CurrentHealth);
            }
        }
    }
}
