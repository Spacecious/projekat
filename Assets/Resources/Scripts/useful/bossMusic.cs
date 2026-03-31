using UnityEngine;

public class bossMusic : MonoBehaviour
{
    [SerializeField] AudioSource bosseMusic;
    void Start()
    {
        bosseMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
