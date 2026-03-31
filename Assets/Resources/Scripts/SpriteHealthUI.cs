using UnityEngine;

public class SpriteHealthUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject[] heartSprites;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < heartSprites.Length; i++)
        {
            // Ako je indeks manji od trenutnog zdravlja, prikaži srce
            if (i < currentHealth)
            {
                heartSprites[i].SetActive(true);
            }
            else
            {
                // U suprotnom ga sakrij
                heartSprites[i].SetActive(false);
            }
        }
    }
}
