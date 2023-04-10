using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: obstacles need a Box Collider component

public class ObstacleCollision : MonoBehaviour
{
    public GameObject player;
    public GameObject characterModel;
    public GameObject mainCamera;
    private float playerGroundYPos;

    void Start()
    {
        playerGroundYPos = player.transform.position.y;
    }

    void OnTriggerEnter(Collider other)
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
    }
}
