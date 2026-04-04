using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    private Slider healthSlider;
    private Transform cam;

    void Awake()
    {
      
        healthSlider = GetComponent<Slider>();
        if (healthSlider == null)
            healthSlider = GetComponentInChildren<Slider>();

        if (Camera.main != null)
            cam = Camera.main.transform;
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        
        if (healthSlider == null)
            healthSlider = GetComponent<Slider>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogError("EnemyHealthUI na " + gameObject.name + " ne može da pronađe Slider komponentu!");
        }
    }

    void LateUpdate()
    {
       
        if (cam != null)
        {
            transform.LookAt(transform.position + cam.forward);
        }
    }
}