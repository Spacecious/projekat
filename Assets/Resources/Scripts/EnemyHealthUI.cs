using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealthUI : MonoBehaviour
{
    private Slider healthSlider;
    private Transform cam;

    void Awake()
    {
        healthSlider = GetComponent<Slider>();
        cam = Camera.main.transform;
    }

    // Funkcija koju pozivamo da osvežimo bar
  public void SetHealth(int currentHealth, int maxHealth)
{
    healthSlider.maxValue = maxHealth;
    healthSlider.value = currentHealth; // Ovo pomera crvenu crtu
}
    void LateUpdate()
    {
        // Čini da HP bar uvek gleda u kameru (da ne bude naopako kad se enemy okrene)
        transform.LookAt(transform.position + cam.forward);
    }
}
