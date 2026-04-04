using UnityEngine;
using UnityEngine.SceneManagement;

public class finalBossDefeat : MonoBehaviour
{
    HealthComponent hp;
    bool hasRun = false;
    
    void Start()
    {
        hp = GetComponent<HealthComponent>();
    }

    
    void Update()
    {
        if (hp != null && hp.CurrentHealth <= 1 && hasRun == false)
        {
            hasRun = true;
            Twist();

        }
    }

    void Twist()
    {
        Screen.SetResolution(960, 540, FullScreenMode.FullScreenWindow);
        Debug.Log("Sta kurac");
        SceneManager.LoadScene("Twist");
    }
}