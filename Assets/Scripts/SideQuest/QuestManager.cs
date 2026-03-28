using UnityEngine;

public class QuestManager : MonoBehaviour
{
    
    private static int collectedItems = 0;
    private static int maxItemsPossible = 10;

    public static void CollectItem()
    {
        collectedItems++;
        Debug.Log("Pokupio si item! Ukupno u ovoj partiji: " + collectedItems);
    }

    public static int GetTotalCollected()
    {
        return collectedItems;
    }
}
