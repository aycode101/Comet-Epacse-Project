using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerObject;
    private Transform playerPosition;
    private PlayerMove playerMovement;

    private float speed;
    private float gapDistance = 3f;
    private Vector3 direction;


    void Start()
    {
        // Get PlayerMove and Transform components from GameObject (set to Player)
        playerMovement = playerObject.GetComponent<PlayerMove>();
        playerPosition = playerObject.GetComponent<Transform>();
    }

    void Update()
    {
        // Turn Temoc to face the player at all times
        direction = (playerPosition.position - transform.position).normalized;
        //transform.LookAt(playerPosition.position);

        // Maintain a consistent gap between Temoc and the player
        if ((transform.position - playerPosition.position).magnitude > gapDistance)
        {
            transform.Translate(direction * Time.deltaTime * speed);
            //transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
        }

        // Make Temoc's speed always slightly faster than the player's (so Temoc won't fall behind)
        speed = playerMovement.moveSpeed * 2f;
    }
}
