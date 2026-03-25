using UnityEngine;
using UnityEngine.SceneManagement;

public class boss1Defeat : MonoBehaviour
{
    HealthComponent hp;
    static public bool bossBeat = false;
    private bool sceneLoad = false;
    void Start()
    {
        hp = GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp != null && hp.CurrentHealth <= 0 && !sceneLoad)
        {
            sceneLoad = true;
            bossBeat = true;
            Invoke("Menu", 0.5f);
            
        }
    }
    
    void Menu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
