using UnityEngine;
using UnityEngine.UI;          // Ovo rešava grešku za 'Image'
using System.Collections;

public class AbilitiesCooldownUI : MonoBehaviour
{
   public Image dashIcon; 
    // Prevuci Gamble_Icon ovde
    public Image gambleIcon; 

    [Header("Colors")]
    // Boja kad je ability spreman (puna bela, Alpha 255)
    public Color readyColor = Color.white; 
    // Boja kad se hladi (tamnija/providnija)
    public Color cooldownColor = new Color(0.5f, 0.5f, 0.5f, 0.7f); 

    void Start()
    {
        // Na početku su oba spremna
        SetIconReady(dashIcon);
        SetIconReady(gambleIcon);
    }

    // Funkcija koja pokreće vizuelni cooldown za Dash
    public void StartDashCooldown(float duration)
    {
        StartCoroutine(CooldownRoutine(dashIcon, duration));
    }

    // Funkcija koja pokreće vizuelni cooldown za Gamble
    public void StartGambleCooldown(float duration)
    {
        StartCoroutine(CooldownRoutine(gambleIcon, duration));
    }

    IEnumerator CooldownRoutine(Image icon, float duration)
    {
        icon.color = cooldownColor; // Potamni ikonicu
        icon.fillAmount = 1; // Krećemo od pune slike koja se "prazni"

        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            // Smanjujemo fillAmount od 1 ka 0 tokom vremena trajanja
            icon.fillAmount = 1 - (timer / duration); 
            yield return null;
        }

        // Kada se završi, ikonica je ponovo spremna
        SetIconReady(icon);
    }

    void SetIconReady(Image icon)
    {
        icon.color = readyColor; // Vrati normalnu boju
        icon.fillAmount = 0; // Prazna (ili 1, zavisi kako želiš, probaj šta ti bolje izgleda)
    }
}
