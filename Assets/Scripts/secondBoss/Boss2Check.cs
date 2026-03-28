using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss2Check : MonoBehaviour
{

    /* Prvi boss*/
    public Image staraSlika2;
    public Sprite achievement2;

    public Image bossBeat2;
    public Button bossButton3;

    private bool firstRun = false;
    void Start()    
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Boss2Defeat.bossBeat2 && firstRun == false)
        {
            staraSlika2.sprite = achievement2;
            bossBeat2.enabled = false;
            bossButton3.gameObject.SetActive(true);
            firstRun = true;
        }



    }


}

