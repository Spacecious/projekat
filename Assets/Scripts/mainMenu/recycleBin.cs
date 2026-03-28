using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class recycleBin : MonoBehaviour
{
    public GameObject tata;
    bool isUpaljen;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClick()
    {
        isUpaljen = !isUpaljen;
        tata.SetActive(isUpaljen);
    }
}
