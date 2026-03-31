using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss3Check : MonoBehaviour
{

    /* Prvi boss*/
    public Image staraSlika3;
    public Sprite achievement3;

    public Image bossBeat3;
    public Button bossButton4;

    private bool firstRun = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Boss3Defeat.bossBeat3 && firstRun == false)
        {
            staraSlika3.sprite = achievement3;
            bossBeat3.enabled = false;
            bossButton4.gameObject.SetActive(true);
            firstRun = true;
        }



    }


}

