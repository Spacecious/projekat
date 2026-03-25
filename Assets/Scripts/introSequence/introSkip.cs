using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class introSkip : MonoBehaviour
{
   

    public void SceneChange()
    {
        StartCoroutine(ChangeSceneRoutine());
    }

    IEnumerator ChangeSceneRoutine()
    {
        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        

        yield return null; // wait 1 frame

        SceneManager.LoadScene("mainMenu");
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SceneChange();
        }
    }
}
