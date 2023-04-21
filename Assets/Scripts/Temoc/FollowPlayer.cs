using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject playerObject;
    private Transform playerPosition;
    private PlayerMove playerMovement;

    private float playerGroundYPos;
    private float speed;

    private float horizontalGap = 3f;
    private float verticalGap = -.3f;
    private float gameOverGap = 1.5f;
    private Vector3 horizontalDirection;
    private Vector3 verticalDirection;
    private float horizontalDistance;
    private float verticalDistance;

    void Start()
    {
        // Get PlayerMove and Transform components from GameObject (set to Player)
        playerMovement = playerObject.GetComponent<PlayerMove>();
        playerPosition = playerObject.GetComponent<Transform>();
        playerGroundYPos = playerPosition.position.y;
    }

    void Update()
    {
        // Get directions from Temoc to the player
        horizontalDirection = (playerPosition.position - transform.position).normalized;
        verticalDirection = horizontalDirection; 
        horizontalDirection.y = 0;
        verticalDirection.x = 0;
        verticalDirection.z = 0;

        // Maintain constant gap between Temoc and the player
        horizontalDistance = Math.Abs(transform.position.x - playerPosition.position.x) + Math.Abs(transform.position.z - playerPosition.position.z);
        if (horizontalDistance > horizontalGap)
        {
            transform.Translate(horizontalDirection * Time.deltaTime * speed);
        }

        verticalDistance = transform.position.y - playerPosition.position.y;
        if ((verticalDistance > verticalGap + 0.05) || (verticalDistance < verticalGap - 0.05))
        {
            transform.Translate(verticalDirection * Time.deltaTime * speed);
            Debug.Log(verticalDirection);
        }

        // Move Temoc when player falls
        GameObject characterModel = GameObject.Find("Player/Ch42_nonPBR@Standard Run").gameObject;
        Animator animator = characterModel.GetComponent<Animator>();
        string current_animation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (current_animation.Equals("Stumble Backwards"))
        {
            if (transform.position.z - playerPosition.position.z < gameOverGap)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            if (transform.position.y > playerGroundYPos + verticalGap)
            {
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }

        // Make Temoc's speed always slightly faster than the player's (so Temoc won't fall behind)
        speed = playerMovement.moveSpeed * 1.3f;
    }
}
