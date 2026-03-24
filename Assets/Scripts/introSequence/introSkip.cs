using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class introSkip : MonoBehaviour
{
    public void SceneChange()
    {

        SceneManager.LoadScene("Slobodan");
    }
    

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            
            SceneChange();
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }
}
