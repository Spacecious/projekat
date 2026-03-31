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
        if(hp != null && hp.CurrentHealth <= 1 && !sceneLoad)
        {
            sceneLoad = true;
            bossBeat = true;
            Invoke("Menu", 0.5f);
            
        }
    }
    
    void Menu()
    {
        Screen.SetResolution(960, 540, FullScreenMode.Windowed);
        SceneManager.LoadScene("mainMenu");
    }
}
