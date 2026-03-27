using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuTaskbar : MonoBehaviour
{
    public GameObject taskBar;
    bool isClicked = false;

    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private const int GWL_STYLE = -16;
    private const int WS_CAPTION = 0x00C00000; // The title bar
    private const int WS_THICKFRAME = 0x00040000; // The resize border
    void Start()
    {
        taskBar.SetActive(false);
        IntPtr hWnd = GetActiveWindow();
        int style = GetWindowLong(hWnd, GWL_STYLE);
        SetWindowLong(hWnd, GWL_STYLE, style & ~WS_CAPTION & ~WS_THICKFRAME);
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
        Screen.SetResolution(640, 320, FullScreenMode.Windowed);
        SceneManager.LoadScene("Slobodan");
        
    }

    
    public void secondBoss()
    {
        int screenWidth = Display.main.systemWidth;
        int screenHeight = Display.main.systemHeight;

        int windowWidth = 1280;
        int windowHeight = 640;

        int posX = 0;
        int posY = 0;

        IntPtr handle = GetActiveWindow();
        SetWindowPos(handle, IntPtr.Zero, posX, posY, windowWidth, windowHeight, 0);
        SceneManager.LoadScene("BorovnicaFinalBoss");
    }
}  
        
    

   /* public void thirdBoss()
    {
        SceneManager.LoadScene("Slobodan");
        Screen.SetResolution(640, 320, FullScreenMode.Windowed);
    }*/

