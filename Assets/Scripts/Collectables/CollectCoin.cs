using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public AudioSource coinFX;

    void OnTriggerEnter(Collider other)
    {
        // Only execute if collided with Player object (has tag "Player")
        if (other.gameObject.tag == "Player")
        {
            coinFX.Play();
            CollectableControl.coinCount += 1;

            this.gameObject.SetActive(false);
        }
    }
}
