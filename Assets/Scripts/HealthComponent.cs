using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{

    [SerializeField]  int MaxHealth;

    private int CurrentHealth;

    public int GetHealth()
    {
        return CurrentHealth;

    }
    void Start()
    {
        CurrentHealth=MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        if (CurrentHealth == 0)
        {
            Destroy(gameObject);
        }

    }
    public void Heal()
    {
        CurrentHealth = Mathf.Min(CurrentHealth + 1, MaxHealth);
        Debug.Log("Healed! Current Health: " + CurrentHealth);
    }

    void Update()
    {
        
    }
}
