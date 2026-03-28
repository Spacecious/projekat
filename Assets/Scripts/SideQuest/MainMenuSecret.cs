using UnityEngine;

public class MainMenuSecret : MonoBehaviour
{
    
    [SerializeField] GameObject secretButton;
    [SerializeField] int requiredItems = 8;

    void Start()
    {
        int collected = QuestManager.GetTotalCollected();
        if(collected >= requiredItems)
        {
            secretButton.SetActive(true);
            Debug.Log("Svaka čast! Tajni video je otključan.");
        }
        else
        {
            secretButton.SetActive(false);
        }
    }

    
}
