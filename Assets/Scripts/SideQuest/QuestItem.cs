using UnityEngine;

public class QuestItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.CollectItem();
            Destroy(gameObject);
        }
    }
}
