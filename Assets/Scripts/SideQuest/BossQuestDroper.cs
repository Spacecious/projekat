using System.Threading;
using UnityEngine;

public class BossQuestDroper : MonoBehaviour
{
    
    [SerializeField] GameObject questItemPrefab;
    [SerializeField] int totalItemsToDrop = 3;
    [SerializeField] float dropInterval = 10f;

    [SerializeField] float minX, maxX, minY, maxY;

    private int itemsDroped = 0;
    private float timer = 0f;

    void Update()
    {
        if(itemsDroped >= totalItemsToDrop)
            return;
        timer += Time.deltaTime;

        if(timer >= dropInterval)
        {
            DropItem();
            timer = 0f;
        }
    }

    void DropItem()
    {
        if(questItemPrefab != null)
        {
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            Instantiate(questItemPrefab, spawnPos, Quaternion.identity);
            itemsDroped++;
            Debug.Log($"Izbačen quest item {itemsDroped}/{totalItemsToDrop}");
        }
    }
}
