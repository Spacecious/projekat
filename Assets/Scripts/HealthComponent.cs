using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{

    [SerializeField]  int MaxHealth;
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
        CurrentHealth=MaxHealth;
        UpdateHeartUI();
        FindHeartsInScene();
        //Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);

        enemyUI = GetComponentInChildren<EnemyHealthUI>();
        if(enemyUI != null) enemyUI.SetHealth(CurrentHealth, MaxHealth);
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
        //dmg.Play();
    
    if (enemyUI != null)
    {
        enemyUI.SetHealth(CurrentHealth, MaxHealth);
    }

    UpdateHeartUI();

    if (CurrentHealth == 0)
    {
        // 1. Proveravamo da li je uništeni objekat Boss pomoću Taga
        if (gameObject.CompareTag("Boss")) 
        {
            UnlockPlayerAbilities();
        }

        // 2. Ako je uništen Player, možda želiš da učitaš Game Over scenu
        if (gameObject.CompareTag("player"))
        {
            Debug.Log("Player je poginuo!");
            SceneManager.LoadScene("mainMenu"); // Primer
        }

        Destroy(gameObject);
    }
        

    }


    private void UnlockPlayerAbilities()
{
    Player.firstBossDefeated = true;

    // Umesto GameObject.Find, nađi Player-a i uzmi njegove reference
    Player p = Object.FindAnyObjectByType<Player>();
    if (p != null)
    {
        if (p.dashIcon != null) p.dashIcon.SetActive(true);
        if (p.gambleIcon != null) p.gambleIcon.SetActive(true);
        
        // Takođe, uključi Gamble UI
        if (p.slotUI != null) p.slotUI.gameObject.SetActive(true);
    }

    Debug.Log("BOSS PORAŽEN! Sposobnosti i UI otključani.");
}

    public void Heal()
    {
        CurrentHealth = Mathf.Min(CurrentHealth + 1, MaxHealth);
        Debug.Log("Healed! Current Health: " + CurrentHealth);
        //Screen.SetResolution(CurrentHealth*360, CurrentHealth*240, FullScreenMode.Windowed);
        UpdateHeartUI();
    }

    public void Heal(int health) {
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
