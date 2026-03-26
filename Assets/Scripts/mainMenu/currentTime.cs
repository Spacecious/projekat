using System;
using TMPro;
using UnityEngine;

public class currentTime : MonoBehaviour
{
    public TextMeshProUGUI timeText; 

    void Update()
    {
        DateTime now = DateTime.Now;

        if (timeText != null)
            timeText.text = now.ToString("HH:mm:ss");
    }

}
