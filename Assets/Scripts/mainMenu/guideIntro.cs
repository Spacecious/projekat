using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuideIntro : MonoBehaviour
{
    public Image clicky;
    [SerializeField] private float fadeSpeed= 1.0f;
    public AudioSource clickyAppear;
    public static bool introOver = false;
    public GameObject clickBlocker;

    void Start()
    {
        if (introOver == false)
        {
            clickBlocker.SetActive(true);
            SetAlpha(0);
            StartCoroutine(FadeIn());
        }
        else
        {
            clickBlocker.SetActive(false);
        }
        
    }
    void SetAlpha(float a)
    {
        if (clicky != null)
        {
            Color c = clicky.color;
            c.a = a;
            clicky.color = c;
        }
    }

    
    IEnumerator FadeIn()
    {
        float alpha = 0;
        clickyAppear.Play();
        while (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed;
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(1);
        
    }

        public IEnumerator FadeOut()
        {
            float alpha = 1;
            while (alpha > 0)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                SetAlpha(alpha);
                yield return null;
            }


            SetAlpha(0);
            clickyAppear.Play();
        overIntro();

        if (clickBlocker != null)
            clickBlocker.SetActive(false);
    }
    void overIntro()
    {
        introOver = true;
    }
}

    

