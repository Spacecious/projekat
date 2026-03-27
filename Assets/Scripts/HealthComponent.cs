using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{

    [SerializeField]  int MaxHealth;
    private GameObject[] heartSprites;
    private int CurrentHealth;
    
   

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
        //Screen.SetResolution(CurrentHealth*360, CurrentHealth*240, FullScreenMode.Windowed);
       
        
        UpdateHeartUI();
        if (CurrentHealth == 0)
        {
            Destroy(gameObject);
        }
        

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
