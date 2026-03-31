using UnityEngine;
using System.Runtime.InteropServices;

public class WindowShake : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(System.IntPtr hWnd, System.IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();

    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_NOZORDER = 0x0004;

    private Vector2Int originalPos;
    private bool isShaking = false;
    private float shakeTimer = 0f;

    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 15f;
    [SerializeField] float shakeSpeed = 0.02f;
    private float shakeInterval = 0f;

    void Start()
    {
        // Save original window position
        originalPos = GetWindowPosition();
    }

    void Update()
    {
        if (isShaking)
        {
            shakeTimer -= Time.deltaTime;
            shakeInterval -= Time.deltaTime;

            if (shakeInterval <= 0f)
            {
                // Move window to random offset
                int offsetX = (int)Random.Range(-shakeMagnitude, shakeMagnitude);
                int offsetY = (int)Random.Range(-shakeMagnitude, shakeMagnitude);
                SetWindowPos(GetActiveWindow(), System.IntPtr.Zero,
                    originalPos.x + offsetX,
                    originalPos.y + offsetY,
                    0, 0, SWP_NOSIZE | SWP_NOZORDER);

                shakeInterval = shakeSpeed;
            }

            if (shakeTimer <= 0f)
            {
                // Reset window to original position
                StopShake();
            }
        }
    }

    public void StartShake()
    {
        if (isShaking) return;
        originalPos = GetWindowPosition();
        isShaking = true;
        shakeTimer = shakeDuration;
    }

    public void StopShake()
    {
        isShaking = false;
        SetWindowPos(GetActiveWindow(), System.IntPtr.Zero,
            originalPos.x, originalPos.y,
            0, 0, SWP_NOSIZE | SWP_NOZORDER);
    }

    private Vector2Int GetWindowPosition()
    {
        // Gets current window position from PlayerPrefs or screen center
        int x = PlayerPrefs.GetInt("WindowX", 100);
        int y = PlayerPrefs.GetInt("WindowY", 100);
        return new Vector2Int(x, y);
    }
}