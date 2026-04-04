using UnityEngine;

public class clickySkip : MonoBehaviour
{
    [SerializeField] GameObject clickyManager;
    [SerializeField] GameObject clickBlock;
    public void clickMe()
    {
        clickyManager.SetActive(false);
        clickBlock.SetActive(false);
    }
}
