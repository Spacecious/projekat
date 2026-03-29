using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    private TextMeshProUGUI ammoText;

    void Awake()
    {
        ammoText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateAmmoDisplay(int currentAmmo)
    {
        if (ammoText != null)
        {
            ammoText.fontSize = 216;
            ammoText.text = currentAmmo.ToString();

        }
    }
}
