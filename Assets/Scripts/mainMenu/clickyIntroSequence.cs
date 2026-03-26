using System.Collections;
using TMPro;
using UnityEngine;

public class clickyIntroSequence : MonoBehaviour
{
    private string fullText;
    public TextMeshProUGUI startText;
    private float time = 2f;
    [SerializeField] GuideIntro cg;
    public AudioSource textSound;
    float min = 0.8f;
    float max = 1.2f;
    
    
    IEnumerator Start()
    {
        if (GuideIntro.introOver == false)
        {
            
            fullText = startText.text;
            startText.text = "";


            foreach (char letter in fullText.ToCharArray())
            {
                startText.text += letter;
                textSound.pitch = Random.Range(min, max);
                textSound.Play();
                if (letter == '.')
                {
                    yield return new WaitForSeconds(time);
                    startText.text = "";
                }
                yield return new WaitForSeconds(0.1f);
            }

            cg.StartCoroutine(cg.FadeOut());
        }
        startText.text = "";
    }
        
    }
       

    
