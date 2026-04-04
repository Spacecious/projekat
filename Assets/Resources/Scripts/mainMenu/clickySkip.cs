using UnityEngine;

public class clickySkip : MonoBehaviour
{
    [SerializeField] GameObject clickyManager;
    public void clickMe()
    {
        clickyManager.SetActive(false);
    }
}
