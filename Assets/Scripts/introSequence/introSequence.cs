using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour
{
    public TextMeshProUGUI startText;
    private string fullText;
    public AudioSource mainMenuText;
    private float min = 0.8f;
    private float max = 1.2f;
    public GameObject spaceBar;
    public Image spaceBars;
    public GameObject pressSpace;

    IEnumerator Start()
    {
        Screen.SetResolution(640, 320, FullScreenMode.Windowed);
        spaceBar.SetActive(false);
        pressSpace.SetActive(false);
        spaceBars.color = new Color(255, 255, 255, 0.3f);

        fullText = startText.text;
        startText.text = "";


        foreach (char letter in fullText.ToCharArray())
        {
            startText.text += letter;
            mainMenuText.pitch = Random.Range(min, max);
            mainMenuText.Play();
            if (letter == '.')
            {
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(0.1f);
        }
        spaceBar.SetActive(true);
        pressSpace.SetActive(true);
    }
}
