using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuTaskbar : MonoBehaviour
{
    public GameObject taskBar;
    bool isClicked = false;
    void Start()
    {
        taskBar.SetActive(false);
    }

    public void OnClick()
    {
        if(isClicked == false)
        {
            taskBar.SetActive(true);
            isClicked = true;
        }
        else
        {
            taskBar.SetActive(false);
            isClicked = false;
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void firstBoss()
    {
        SceneManager.LoadScene("Slobodan");
        Screen.SetResolution(640, 320, FullScreenMode.Windowed);
    }

    public void secondBoss()
    {
        SceneManager.LoadScene("Monako");
        Screen.SetResolution(1280, 640, FullScreenMode.Windowed);
    }

   /* public void thirdBoss()
    {
        SceneManager.LoadScene("Slobodan");
        Screen.SetResolution(640, 320, FullScreenMode.Windowed);
    }*/
}
