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
    }

    void FindHeartsInScene()
    {
        heartSprites = new GameObject[3]; 

        
        heartSprites[0] = GameObject.Find("Health_1");
        heartSprites[1] = GameObject.Find("Health_2");
        heartSprites[2] = GameObject.Find("Health_3");

        // Sigurnosna provera
        if (heartSprites[0] == null) Debug.LogError("Nisam našao Health_1 u sceni!");
    }


    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

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
