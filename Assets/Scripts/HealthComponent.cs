using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{

    [SerializeField]  int MaxHealth;

    public int CurrentHealth;

    public UnityEvent<float> OnHealthChanged;

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
        OnHealthChanged.Invoke(CurrentHealth);

        KupinaBoss boss = GetComponent<KupinaBoss>();
        if(boss != null)
        {
            boss.UpdateVineCount(CurrentHealth);
        }

        if (CurrentHealth <= 0)
        {
            Debug.Log("Dead");
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
