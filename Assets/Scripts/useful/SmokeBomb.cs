using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    [SerializeField] GameObject smokeBomb;
    AudioSource fall;
    void Start()
    {
        
    }
    void SpawnSmoke()
    {
        fall.Play();
        Vector3 pos = transform.position;
        GameObject smoke = Instantiate(smokeBomb, pos, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
