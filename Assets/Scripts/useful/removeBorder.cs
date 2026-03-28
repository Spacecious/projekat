using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class removeBorder : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private const int GWL_STYLE = -16;
    private const int WS_CAPTION = 0x00C00000; // The title bar
    private const int WS_THICKFRAME = 0x00040000; // The resize border
    void Awake()
    {
        IntPtr hWnd = GetActiveWindow();
        int style = GetWindowLong(hWnd, GWL_STYLE);

        // Remove the title bar and resizing borders
        SetWindowLong(hWnd, GWL_STYLE, style & ~WS_CAPTION & ~WS_THICKFRAME);
    }

    
}
