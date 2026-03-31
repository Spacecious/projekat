using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{

    [SerializeField] int MaxHealth;
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
        UpdateHeartUI();
        FindHeartsInScene();
        //Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);

        enemyUI = GetComponentInChildren<EnemyHealthUI>();
        if (enemyUI != null) enemyUI.SetHealth(CurrentHealth, MaxHealth);
    }

    void FindHeartsInScene()
    {
        heartSprites = new GameObject[3];


        heartSprites[0] = GameObject.Find("Health_1");
        heartSprites[1] = GameObject.Find("Health_2");
        heartSprites[2] = GameObject.Find("Health_3");

        // Sigurnosna provera
        if (heartSprites[0] == null) Debug.LogError("Nisam na�ao Health_1 u sceni!");
    }


    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        dmg.pitch = Random.Range(0.8f, 1.2f);
        dmg.Play();

        if (enemyUI != null)
        {
            enemyUI.SetHealth(CurrentHealth, MaxHealth);
        }

        if (CompareTag("Player"))
        {
            UpdateHeartUI();

        }

        if (CurrentHealth == 0)
        {

            
            if (gameObject.CompareTag("Boss"))
            {
                UnlockPlayerAbilities();
                SceneManager.LoadScene("mainMenu");
                
            }

            
            if (gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("mainMenu");


                Debug.Log("Player je poginuo!");
                // Primer
            }

            Destroy(gameObject);
        }


    }


    private void UnlockPlayerAbilities()
    {
        Player.firstBossDefeated = true;

        
        Player p = Object.FindAnyObjectByType<Player>();
        if (p != null)
        {
            if (p.dashIcon != null) p.dashIcon.SetActive(true);
            if (p.gambleIcon != null) p.gambleIcon.SetActive(true);

            
            if (p.slotUI != null) p.slotUI.gameObject.SetActive(true);
        }

        Debug.Log("BOSS PORAŽEN! Sposobnosti i UI otključani.");
    }

    public void Heal()
    {
        CurrentHealth = Mathf.Min(CurrentHealth + 1, MaxHealth);
        UpdateHeartUI();
    }

    public void Heal(int health)
    {
        CurrentHealth += health;
    }

    void Update()
    {

    }

    void UpdateHeartUI()
    {
        if (heartSprites == null || heartSprites.Length == 0) return;


        for (int i = 0; i < heartSprites.Length; i++)
        {

            if (i < CurrentHealth)
            {
                if (heartSprites[i] != null) heartSprites[i].SetActive(true);
            }

            else
            {
                if (heartSprites[i] != null) heartSprites[i].SetActive(false);
            }
        }

    }
}
