using UnityEngine;
using UnityEngine.UI;          // Ovo rešava grešku za 'Image'
using System.Collections;

public class SlotMachineUI : MonoBehaviour
{
    public Image[] slotImages; // Prevuci Slot_1, Slot_2, Slot_3 ovde
    public Sprite[] symbols;

    [SerializeField] float spinDuration = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpinning(int finalS1, int finalS2, int finalS3)
    {
        StartCoroutine(SpinRoutine(finalS1, finalS2, finalS3));
    }

    IEnumerator SpinRoutine(int s1, int s2, int s3)
    {
        float timer = 0;
        
        // Dok traje spinDuration, menjaj slike nasumično (efekat vrtnje)
        while (timer < spinDuration)
        {
            slotImages[0].sprite = symbols[Random.Range(0, symbols.Length)];
            slotImages[1].sprite = symbols[Random.Range(0, symbols.Length)];
            slotImages[2].sprite = symbols[Random.Range(0, symbols.Length)];
            
            timer += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        // Na kraju postavi prave rezultate koje je igra izračunala
        slotImages[0].sprite = symbols[s1];
        slotImages[1].sprite = symbols[s2];
        slotImages[2].sprite = symbols[s3];
        
        Debug.Log("Slotovi su se zaustavili!");
    }
}
