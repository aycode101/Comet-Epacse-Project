using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometPowerUp : MonoBehaviour
{
    public int powerUpTime = 5;
    private float speedIncrease = 1.5f;
    PlayerMove prevSpeed;
    GameObject player;
    public AudioSource woosh;
    
    // Start is called before the first frame update
    void Start()
    {
        woosh = GameObject.Find("LevelControl/Woosh").GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Only execute if collided with Player object (has tag "Player")
        if (other.gameObject.tag == "Player")
        {            
            //get the game object
            player = other.gameObject;
            woosh.Play();
            //get the player script to access and modify the move speed
            prevSpeed = player.GetComponent<PlayerMove>();
            prevSpeed.moveSpeed *= speedIncrease;

            //add trail to the player
            player.transform.GetChild(2).gameObject.SetActive(true);
            
            //make the power-up inactive and wait until call the power down function when it wears off
            this.gameObject.SetActive(false);
            Invoke("powerDown", powerUpTime);
        }
    }

    void powerDown()
    {
        //hide the trail
        player.transform.GetChild(2).gameObject.SetActive(false);
        //restore the previous speed
        prevSpeed.moveSpeed /= speedIncrease;
    }
}
