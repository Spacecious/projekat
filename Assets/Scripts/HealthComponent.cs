using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{

    [SerializeField]  int MaxHealth;
    private GameObject[] heartSprites;
    public int CurrentHealth;
    
   

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
            // SceneManager.LoadScene("GameOver"); // Primer
        }

        Destroy(gameObject);     
    }
        

    }


    private void UnlockPlayerAbilities()
{
    // Postavljamo statičku varijablu koju smo dogovorili
    Player.firstBossDefeated = true;

    // Pronalazimo ikonice i aktiviramo ih
    GameObject dashIcon = GameObject.Find("DashIcon");
    GameObject gambleIcon = GameObject.Find("GambleIcon");

    if (dashIcon != null) dashIcon.SetActive(true);
    if (gambleIcon != null) gambleIcon.SetActive(true);

    Debug.Log("BOSS PORAŽEN! Sposobnosti otključane.");
}

    public void Heal()
    {
        CurrentHealth = Mathf.Min(CurrentHealth + 1, MaxHealth);
        Debug.Log("Healed! Current Health: " + CurrentHealth);
        //Screen.SetResolution(CurrentHealth*360, CurrentHealth*240, FullScreenMode.Windowed);
        UpdateHeartUI();
    }

    void Update()
    {
        
    }

    void UpdateHeartUI()
    {
        if (heartSprites == null || heartSprites.Length == 0) return;

        /*
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
        }*/

        for (int i = 0; i < heartSprites.Length; i++)
            {
                if (heartSprites[i] != null)
                {
                    // Srce je aktivno samo ako je njegov redni broj (i) manji od trenutnog zdravlja
                    // npr. za Health 2: i=0 (True), i=1 (True), i=2 (False)
                    heartSprites[i].SetActive(i < CurrentHealth);
                }
            }
       
    }
}
