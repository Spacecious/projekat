using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class bossDefeatedCheck : MonoBehaviour
{

    public Image staraSlika1;
    public Sprite achievement1;

    public Image bossBeat1;
    public Button bossButton2;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(boss1Defeat.bossBeat)
        {
            staraSlika1.sprite = achievement1;
            bossBeat1.enabled = false;
            bossButton2.gameObject.SetActive(true);
        }
         
    }

   
}
