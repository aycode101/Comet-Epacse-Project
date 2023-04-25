using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinDing;

    void Start()
    {
        coinDing = GameObject.Find("LevelControl/CoinCollect").GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        // Only execute if collided with Player object (has tag "Player")
        if (other.gameObject.tag == "Player")
        {
            coinDing.Play();
            CollectableControl.coinCount += 1;

            this.gameObject.SetActive(false);
        }
    }
}
