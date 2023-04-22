using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: obstacles need a Box Collider component

public class ObstacleCollision : MonoBehaviour
{
    private GameObject player;
    private GameObject characterModel;
    private GameObject mainCamera;
    private float playerGroundYPos;
    public GameObject levelControl;
    public AudioSource LargeThump;
    public AudioSource reaction;

    void Start()
    {
        player = GameObject.Find("Player"); 
        characterModel = player.transform.Find("Ch42_nonPBR@Standard Run").gameObject;
        mainCamera = player.transform.Find("Main Camera").gameObject;
        playerGroundYPos = player.transform.position.y;
    }

    void OnTriggerEnter(Collider other)
    {
        // Only execute if collided with Player object (has tag "Player")
        if (other.gameObject.tag == "Player")
        {
            // Prevent collision from triggering infinitely
            this.gameObject.GetComponent<BoxCollider>().enabled = false;


            // Make player stop and fall over
            player.GetComponent<PlayerMove>().enabled = false;
            characterModel.GetComponent<Animator>().Play("Stumble Backwards");

            // Make player model fall to the ground if the collision was in mid-air
            while (player.transform.position.y > playerGroundYPos)
            {
                player.transform.Translate(new Vector3(0, -10f, 0) * Time.deltaTime);
            }

            mainCamera.GetComponent<Animator>().enabled = true;
            levelControl.GetComponent<EndRunSequence>().enabled = true;

            LargeThump.Play();
            reaction.Play();
        }
    }
}
