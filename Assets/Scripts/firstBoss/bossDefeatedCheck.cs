using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class bossDefeatedCheck : MonoBehaviour
{

    /* Prvi boss*/public Image staraSlika1;
    public Sprite achievement1;

    public Image bossBeat1;
    public Button bossButton2;

    private bool firstRun = false;


    void Start()
    {

    }

    // Update is called once per frame
    void OnEnable()
    {
        if(boss1Defeat.bossBeat && firstRun == false)
        {
            staraSlika1.sprite = achievement1;
            firstRun = true;
            bossBeat1.enabled = false;
            bossButton2.gameObject.SetActive(true);
            
        }

        

    }

   
}
