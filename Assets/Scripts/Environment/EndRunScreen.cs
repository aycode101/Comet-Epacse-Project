using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRunScreen : MonoBehaviour
{
    public GameObject liveCoins;
    public GameObject endScreen;
    public GameObject fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndSequence());
    
    }

    // Update is called once per frame
    IEnumerator EndSequence() 
    {
        yield return new WaitForSeconds(5);
        liveCoins.SetActive(false);
        endScreen.SetActive(true);
        yield return new WaitForSeconds(5);
        fadeOut.SetActive(true);
    }
}
